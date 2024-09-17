using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Orders.Events;

internal sealed class OrderCreatedDomainEventHandler(
    ILogger<OrderCreatedDomainEventHandler> _logger,
    IRepository<Order> _orderRepository,
    IOrderPlacementEvaluator _orderPlacementEvaluator,
    IUnitOfWork _unitOfWork) :
    IDomainEventHandler<OrderCreatedDomainEvent>
{
    public async Task Handle(
        OrderCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("ORDER CREATED: {tradeType} {quantity} {symbol} @ {price} {orderType} {timeInForce}",
            domainEvent.TradeType,
            domainEvent.Quantity,
            domainEvent.Symbol,
            domainEvent.OrderType,
            domainEvent.OrderType,
            domainEvent.TimeInForce);

        var maybeOrder = await _orderRepository
            .FirstOrDefaultAsync(o => o.Id == domainEvent.OrderId, cancellationToken)
            .ThrowIfNull();

        var order = maybeOrder.Value;

        var evaluationResult = await _orderPlacementEvaluator.EvaluateAsync(order, cancellationToken);

        if (evaluationResult.IsFailure)
        {
            var rejectResult = order.Reject(evaluationResult.Error.Message);

            if (rejectResult.IsFailure)
            {
                _logger.LogError("Failed to reject order: {error}", rejectResult.Error);

                return;
            }

            _orderRepository.Update(order);
        }

        var acceptResult = order.Accept();

        if (acceptResult.IsFailure)
        {
            _logger.LogError("Failed to accept order: {error}", acceptResult.Error);

            return;
        }

        _orderRepository.Update(order);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}