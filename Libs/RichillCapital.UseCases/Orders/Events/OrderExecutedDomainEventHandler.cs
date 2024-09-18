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
    IRepository<Trade> _tradeRepository,
    IUnitOfWork _unitOfWork) :
    IDomainEventHandler<OrderExecutedDomainEvent>
{
    public async Task Handle(
        OrderExecutedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "ORDER EXECUTED: {tradeType} {executionQuantity} {symbol} @ {executionPrice} {orderType} {timeInForce}",
            domainEvent.TradeType,
            domainEvent.Quantity,
            domainEvent.Symbol,
            domainEvent.Price,
            domainEvent.OrderType,
            domainEvent.TimeInForce);

        var (commission, tax) = CalculateCommissionAndTax();

        var execution = Execution
            .Create(
                ExecutionId.NewExecutionId(),
                domainEvent.AccountId,
                domainEvent.OrderId,
                domainEvent.Symbol,
                domainEvent.TradeType,
                domainEvent.OrderType,
                domainEvent.TimeInForce,
                domainEvent.Quantity,
                domainEvent.Price,
                commission,
                tax,
                domainEvent.OccurredTime)
            .ThrowIfError()
            .Value;

        _executionRepository.Add(execution);

        await HandleUsingFlatToFlatAsync(domainEvent, commission, tax, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task HandleUsingFlatToFlatAsync(
        OrderExecutedDomainEvent domainEvent,
        decimal commission,
        decimal tax,
        CancellationToken cancellationToken = default)
    {
        if (!await _positionRepository.AnyAsync(
            p => p.AccountId == domainEvent.AccountId && p.Symbol == domainEvent.Symbol,
            cancellationToken))
        {
            OpenPositionFlatToFlat(domainEvent, commission, tax);
        }
        else
        {
            var maybePosition = await _positionRepository
                .FirstOrDefaultAsync(
                    p => p.AccountId == domainEvent.AccountId && p.Symbol == domainEvent.Symbol,
                    cancellationToken)
                .ThrowIfNull();

            var existingPosition = maybePosition.Value;

            if (domainEvent.TradeType.HasSameDirectionAs(existingPosition))
            {
                IncreasePositionFlatToFlat(domainEvent, existingPosition, commission, tax);
            }
            else
            {
                var newQuantity = existingPosition.Quantity - domainEvent.Quantity;

                if (newQuantity > 0)
                {
                    DecreasePositionFlatToFlat(domainEvent, existingPosition, commission, tax);
                }
                else if (newQuantity == 0)
                {
                    ClosePositionFlatToFlat(domainEvent, existingPosition, commission, tax);
                }
                else
                {
                    ReversePositionFlatToFlat(domainEvent, existingPosition, commission, tax);
                }
            }
        }
    }

    private void OpenPositionFlatToFlat(
        OrderExecutedDomainEvent domainEvent,
        decimal commission,
        decimal tax)
    {
        var newPosition = Position
            .Create(
                PositionId.NewPositionId(),
                domainEvent.AccountId,
                domainEvent.Symbol,
                domainEvent.TradeType == TradeType.Buy ? Side.Long : Side.Short,
                domainEvent.Quantity,
                domainEvent.Price,
                commission,
                tax,
                decimal.Zero,
                domainEvent.OccurredTime)
            .ThrowIfError()
            .Value;

        _positionRepository.Add(newPosition);
    }

    private void IncreasePositionFlatToFlat(
        OrderExecutedDomainEvent domainEvent,
        Position position,
        decimal commission,
        decimal tax)
    {
        var newQuantity = domainEvent.Quantity + position.Quantity;
        var newAveragePrice = (domainEvent.Quantity * domainEvent.Price + position.Quantity * position.AveragePrice) / newQuantity;
        var newCommission = commission + position.Commission;
        var newTax = tax + position.Tax;

        position.Update(
            newQuantity,
            newAveragePrice,
            newCommission,
            newTax,
            position.Swap);

        _positionRepository.Update(position);
    }

    private void DecreasePositionFlatToFlat(
        OrderExecutedDomainEvent domainEvent,
        Position position,
        decimal commission,
        decimal tax)
    {
        var newQuantity = position.Quantity - domainEvent.Quantity;
        position.Update(newQuantity, position.AveragePrice, position.Commission + commission, position.Tax + tax, position.Swap);
        _positionRepository.Update(position);
    }

    private void ClosePositionFlatToFlat(
        OrderExecutedDomainEvent domainEvent,
        Position position,
        decimal commission,
        decimal tax)
    {
        position.Update(
            position.Quantity - domainEvent.Quantity,
            position.AveragePrice,
            position.Commission + commission,
            position.Tax + tax,
            position.Swap);

        position.Close();
        _positionRepository.Update(position);

        var trade = Trade
            .Create(
                TradeId.NewTradeId(),
                position.AccountId,
                position.Symbol,
                position.Side,
                quantity: position.Quantity, // Max position quantity 
                entryPrice: position.AveragePrice, // Price of first execution 
                entryTimeUtc: position.CreatedTimeUtc,
                exitPrice: domainEvent.Price, // Price of last execution
                exitTimeUtc: domainEvent.OccurredTime,
                commission: position.Commission,
                tax: position.Tax,
                swap: position.Swap)
            .ThrowIfError()
            .Value;

        _tradeRepository.Add(trade);
    }

    private void ReversePositionFlatToFlat(
        OrderExecutedDomainEvent domainEvent,
        Position position,
        decimal commission,
        decimal tax)
    {
        ClosePositionFlatToFlat(domainEvent, position, commission, tax);

        var reversedQuantity = Math.Abs(position.Quantity - domainEvent.Quantity);

        var reversedPosition = Position
            .Create(
                PositionId.NewPositionId(),
                position.AccountId,
                position.Symbol,
                position.Side.Reverse(),
                reversedQuantity,
                domainEvent.Price,
                commission,
                tax,
                decimal.Zero,
                domainEvent.OccurredTime)
            .ThrowIfError()
            .Value;

        _positionRepository.Add(reversedPosition);
    }

    private (decimal Commission, decimal Tax) CalculateCommissionAndTax() =>
        (decimal.Zero, decimal.Zero);
}