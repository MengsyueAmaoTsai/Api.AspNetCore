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
            _logger.LogInformation(
                "Adding to existing position for account id: {accountId} and symbol: {symbol}",
                domainEvent.AccountId,
                domainEvent.Symbol);
        }
        else
        {
            _logger.LogInformation(
                "Closing existing position for account id: {accountId} and symbol: {symbol}",
                domainEvent.AccountId,
                domainEvent.Symbol);

            var newQuantity = openPosition.Quantity - domainEvent.Quantity;

            if (newQuantity < 0)
            {
                _logger.LogInformation("Reversing position for account id: {accountId} and symbol: {symbol}", domainEvent.AccountId, domainEvent.Symbol);
            }
            else if (newQuantity == 0)
            {
                _logger.LogInformation("Closing position for account id: {accountId} and symbol: {symbol}", domainEvent.AccountId, domainEvent.Symbol);
            }
            else
            {
                _logger.LogInformation("Partially closing position for account id: {accountId} and symbol: {symbol}", domainEvent.AccountId, domainEvent.Symbol);
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}