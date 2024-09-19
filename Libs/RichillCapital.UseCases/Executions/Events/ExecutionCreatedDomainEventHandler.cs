using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Executions.Events;

internal sealed class ExecutionCreatedDomainEventHandler(
    ILogger<ExecutionCreatedDomainEventHandler> _logger,
    IPositionManager _positionManager,
    IRepository<Position> _positionRepository,
    IRepository<Trade> _tradeRepository,
    IUnitOfWork _unitOfWork) :
    IDomainEventHandler<ExecutionCreatedDomainEvent>
{
    public async Task Handle(
        ExecutionCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "[ExecutionCreated] {tradeType} {quantity} {symbol} @ {price} {orderType} {timeInForce} for execution id: {executionId}",
            domainEvent.TradeType,
            domainEvent.Quantity,
            domainEvent.Symbol,
            domainEvent.Price,
            domainEvent.OrderType,
            domainEvent.TimeInForce,
            domainEvent.ExecutionId);

        var openPositionResult = await _positionManager.GetOpenPositionAsync(
            domainEvent.AccountId,
            domainEvent.Symbol,
            cancellationToken);

        if (openPositionResult.IsFailure)
        {
            if (openPositionResult.Error.Type != ErrorType.NotFound)
            {
                _logger.LogError(
                    "Failed to get open position for account id: {accountId} and symbol: {symbol}",
                    domainEvent.AccountId,
                    domainEvent.Symbol);
            }
            else
            {
                OpenPosition(
                    domainEvent.AccountId,
                    domainEvent.Symbol,
                    domainEvent.TradeType.ToSide(),
                    domainEvent.Quantity,
                    domainEvent.Price,
                    domainEvent.Commission,
                    domainEvent.Tax,
                    domainEvent.OccurredTime);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return;
            }
        }

        var openPosition = openPositionResult.Value;

        if (openPosition.HasSameDirectionAs(domainEvent.TradeType))
        {
            var increaseResult = openPosition.Increase(
                domainEvent.Quantity,
                domainEvent.Price,
                domainEvent.Commission,
                domainEvent.Tax);

            if (increaseResult.IsFailure)
            {
                _logger.LogError(
                    "Failed to increase position for account id: {accountId} and symbol: {symbol}",
                    domainEvent.AccountId,
                    domainEvent.Symbol);

                return;
            }
        }
        else
        {
            var newQuantity = openPosition.Quantity - domainEvent.Quantity;

            if (newQuantity < 0)
            {
                ReservePosition(
                    openPosition,
                    domainEvent.Quantity,
                    domainEvent.Price,
                    domainEvent.Commission,
                    domainEvent.Tax,
                    domainEvent.OccurredTime);
            }
            else if (newQuantity == 0)
            {
                ClosePosition(
                    openPosition,
                    domainEvent.Quantity,
                    domainEvent.Commission,
                    domainEvent.Tax);
            }
            else
            {
                ReducePosition(
                    openPosition,
                    domainEvent.Quantity,
                    domainEvent.Price,
                    domainEvent.Commission,
                    domainEvent.Tax);
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private void OpenPosition(
        AccountId accountId,
        Symbol symbol,
        Side side,
        decimal quantity,
        decimal price,
        decimal commission,
        decimal tax,
        DateTimeOffset executionTimeUtc)
    {
        var newPosition = Position
            .Create(
                PositionId.NewPositionId(),
                accountId,
                symbol,
                side,
                quantity,
                price,
                commission,
                tax,
                decimal.Zero,
                PositionStatus.Open,
                executionTimeUtc)
            .ThrowIfError()
            .Value;

        _positionRepository.Add(newPosition);

    }
    private void ReservePosition(
        Position existingPosition,
        decimal executionQuantity,
        decimal executionPrice,
        decimal commission,
        decimal tax,
        DateTimeOffset executionTimeUtc)
    {
        var newQuantity = existingPosition.Quantity - executionQuantity;

        ClosePosition(
            existingPosition,
            executionQuantity,
            commission,
            tax);

        var newPosition = Position
            .Create(
                PositionId.NewPositionId(),
                existingPosition.AccountId,
                existingPosition.Symbol,
                existingPosition.Side.Reverse(),
                Math.Abs(newQuantity),
                executionPrice,
                commission,
                tax,
                decimal.Zero,
                PositionStatus.Open,
                executionTimeUtc)
            .ThrowIfError()
            .Value;

        _positionRepository.Add(newPosition);
    }

    private void ClosePosition(
        Position position,
        decimal executionQuantity,
        decimal commission,
        decimal tax)
    {
        var result = position.Close(executionQuantity, commission, tax);

        if (result.IsFailure)
        {
            _logger.LogError(
                "Failed to close position for account id: {accountId} and symbol: {symbol}",
                position.AccountId,
                position.Symbol);

            throw new InvalidOperationException("Failed to close position");
        }

        _positionRepository.Update(position);
    }

    private void ReducePosition(
        Position position,
        decimal executionQuantity,
        decimal executionPrice,
        decimal commission,
        decimal tax)
    {
        var result = position.Reduce(executionQuantity, executionPrice, commission, tax);

        if (result.IsFailure)
        {
            _logger.LogError(
                "Failed to reduce position for account id: {accountId} and symbol: {symbol}",
                position.AccountId,
                position.Symbol);

            throw new InvalidOperationException("Failed to reduce position");
        }

        _positionRepository.Update(position);
    }
}