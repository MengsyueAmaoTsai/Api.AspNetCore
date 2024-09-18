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
                var newPosition = Position
                    .Create(
                        PositionId.NewPositionId(),
                        domainEvent.AccountId,
                        domainEvent.Symbol,
                        domainEvent.TradeType.ToSide(),
                        domainEvent.Quantity,
                        domainEvent.Price,
                        domainEvent.Commission,
                        domainEvent.Tax,
                        decimal.Zero,
                        PositionStatus.Open,
                        domainEvent.OccurredTime)
                    .ThrowIfError()
                    .Value;

                _positionRepository.Add(newPosition);
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
                var closeResult = openPosition.Close(domainEvent.Quantity, domainEvent.Commission, domainEvent.Tax);

                if (closeResult.IsFailure)
                {
                    _logger.LogError(
                        "Failed to close position for account id: {accountId} and symbol: {symbol}",
                        domainEvent.AccountId,
                        domainEvent.Symbol);

                    return;
                }

                _positionRepository.Update(openPosition);

                var newPosition = Position
                    .Create(
                        PositionId.NewPositionId(),
                        openPosition.AccountId,
                        openPosition.Symbol,
                        openPosition.Side.Reverse(),
                        Math.Abs(newQuantity),
                        domainEvent.Price,
                        domainEvent.Commission,
                        domainEvent.Tax,
                        decimal.Zero,
                        PositionStatus.Open,
                        domainEvent.OccurredTime)
                    .ThrowIfError()
                    .Value;

                _positionRepository.Add(newPosition);
            }
            else if (newQuantity == 0)
            {
                var closeResult = openPosition.Close(domainEvent.Quantity, domainEvent.Commission, domainEvent.Tax);

                if (closeResult.IsFailure)
                {
                    _logger.LogError(
                        "Failed to close position for account id: {accountId} and symbol: {symbol}",
                        domainEvent.AccountId,
                        domainEvent.Symbol);

                    return;
                }

                _positionRepository.Update(openPosition);
            }
            else
            {
                var reduceResult = openPosition.Reduce(
                    domainEvent.Quantity,
                    domainEvent.Price,
                    domainEvent.Commission,
                    domainEvent.Tax);

                if (reduceResult.IsFailure)
                {
                    _logger.LogError(
                        "Failed to reduce position for account id: {accountId} and symbol: {symbol}",
                        domainEvent.AccountId,
                        domainEvent.Symbol);

                    return;
                }
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}