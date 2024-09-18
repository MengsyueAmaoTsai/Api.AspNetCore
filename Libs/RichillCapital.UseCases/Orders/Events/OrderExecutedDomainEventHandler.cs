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
        _logger.LogInformation("ORDER EXECUTED: {tradeType} {executionQuantity} {symbol} @ {executionPrice} {orderType} {timeInForce}",
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

        // Flat to flat implementation
        if (!await _positionRepository.AnyAsync(
            p => p.AccountId == domainEvent.AccountId && p.Symbol == domainEvent.Symbol,
            cancellationToken))
        {
            // Create new position
            var positionId = PositionId.NewPositionId();

            var newPosition = Position
                .Create(
                    positionId,
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
                var newQuantity = domainEvent.Quantity + existingPosition.Quantity;
                var newAveragePrice = (domainEvent.Quantity * domainEvent.Price + existingPosition.Quantity * existingPosition.AveragePrice) / newQuantity;
                var newCommission = commission + existingPosition.Commission;
                var newTax = tax + existingPosition.Tax;

                existingPosition.Update(
                    newQuantity,
                    newAveragePrice,
                    newCommission,
                    newTax,
                    existingPosition.Swap);
            }
            else
            {
                // Decrease position or reverse position
                var newQuantity = existingPosition.Quantity - domainEvent.Quantity;
                var newCommission = commission + existingPosition.Commission;
                var newTax = tax + existingPosition.Tax;

                existingPosition.Update(
                    newQuantity < 0 ? existingPosition.Quantity : newQuantity,
                    existingPosition.AveragePrice,
                    newCommission,
                    newTax,
                    existingPosition.Swap);

                if (newQuantity == 0)
                {
                    existingPosition.Close();
                }

                if (newQuantity < 0)
                {
                    _logger.LogWarning("RESERVE POSITION: {side} {symbol}",
                        existingPosition.Side,
                        existingPosition.Symbol);
                }
            }

            _positionRepository.Update(existingPosition);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private (decimal Commission, decimal Tax) CalculateCommissionAndTax() =>
        (decimal.Zero, decimal.Zero);
}