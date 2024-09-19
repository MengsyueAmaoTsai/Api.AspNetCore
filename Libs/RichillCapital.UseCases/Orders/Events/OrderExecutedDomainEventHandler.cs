using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Orders.Events;

internal sealed class OrderExecutedDomainEventHandler(
    ILogger<OrderExecutedDomainEventHandler> _logger,
    IRepository<Execution> _executionRepository,
    IRepository<Position> _positionRepository,
    IUnitOfWork _unitOfWork) :
    IDomainEventHandler<OrderExecutedDomainEvent>
{
    public async Task Handle(
        OrderExecutedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        LogEvent(domainEvent);

        var positionSide = domainEvent.TradeType.ToSide();
        var executionQuantity = domainEvent.Quantity;
        var executionPrice = domainEvent.Price;
        var commission = decimal.Zero;
        var tax = decimal.Zero;
        var executionTime = domainEvent.OccurredTime;

        var maybePosition = await _positionRepository.FirstOrDefaultAsync(
            p => p.AccountId == domainEvent.AccountId &&
                p.Symbol == domainEvent.Symbol &&
                p.Status == PositionStatus.Open,
            cancellationToken);

        if (maybePosition.IsNull)
        {
            var positionId = PositionId.NewPositionId();

            var executionOfNewPosition = Execution
                .Create(
                    ExecutionId.NewExecutionId(),
                    domainEvent.AccountId,
                    domainEvent.OrderId,
                    positionId,
                    domainEvent.Symbol,
                    domainEvent.TradeType,
                    domainEvent.OrderType,
                    domainEvent.TimeInForce,
                    domainEvent.Quantity,
                    domainEvent.Price,
                    commission,
                    tax,
                    executionTime)
                .ThrowIfError()
                .Then(_executionRepository.Add)
                .Value;

            var newPosition = Position
                .Create(
                    positionId,
                    domainEvent.AccountId,
                    domainEvent.Symbol,
                    positionSide,
                    executionQuantity,
                    executionPrice,
                    commission,
                    tax,
                    decimal.Zero,
                    PositionStatus.Open,
                    executionTime)
                .ThrowIfError()
                .Then(_positionRepository.Add)
                .Value;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return;
        }

        // Update existing position
        var openPosition = maybePosition.Value;

        var executionOfExistingPosition = Execution
            .Create(
                ExecutionId.NewExecutionId(),
                domainEvent.AccountId,
                domainEvent.OrderId,
                openPosition.Id,
                domainEvent.Symbol,
                domainEvent.TradeType,
                domainEvent.OrderType,
                domainEvent.TimeInForce,
                domainEvent.Quantity,
                domainEvent.Price,
                commission,
                tax,
                executionTime)
            .ThrowIfError()
            .Then(_executionRepository.Add)
            .Value;

        _executionRepository.Add(executionOfExistingPosition);

        if (openPosition.HasSameDirectionAs(domainEvent.TradeType))
        {
            IncreasePosition(
                openPosition,
                executionQuantity,
                executionPrice,
                commission,
                tax);
        }
        else
        {
            var finalQuantity = openPosition.Quantity - executionQuantity;

            if (finalQuantity > 0)
            {
                ReducePosition(
                    openPosition,
                    executionQuantity,
                    executionPrice,
                    commission,
                    tax);
            }
            else if (finalQuantity == 0)
            {
                ClosePosition(
                    openPosition,
                    executionQuantity,
                    commission,
                    tax);
            }
            else
            {
                ReservePosition(
                    openPosition,
                    executionQuantity,
                    executionPrice,
                    commission,
                    tax,
                    executionTime);
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private void LogEvent(OrderExecutedDomainEvent domainEvent) =>
        _logger.LogInformation(
            "[OrderExecuted] {tradeType} {quantity} {symbol} @ {price} {orderType} {timeInForce} for order id: {orderId}",
            domainEvent.TradeType,
            domainEvent.Quantity,
            domainEvent.Symbol,
            domainEvent.Price,
            domainEvent.OrderType,
            domainEvent.TimeInForce,
            domainEvent.OrderId);

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
            throw new InvalidOperationException(result.Error.Message);
        }

        _positionRepository.Update(position);
    }

    private void IncreasePosition(
        Position position,
        decimal executionQuantity,
        decimal executionPrice,
        decimal commission,
        decimal tax)
    {
        var result = position.Increase(executionQuantity, executionPrice, commission, tax);

        if (result.IsFailure)
        {
            throw new InvalidOperationException(result.Error.Message);
        }

        _positionRepository.Update(position);
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
            throw new InvalidOperationException(result.Error.Message);
        }

        _positionRepository.Update(position);
    }
}