using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Events;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Orders.Events;

internal sealed class OrderRejectedDomainEventHandler(
    ILogger<OrderRejectedDomainEventHandler> _logger) :
    IDomainEventHandler<OrderRejectedDomainEvent>
{
    public Task Handle(
        OrderRejectedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("[OrderRejected] {tradeType} {quantity} {symbol} @ {price} {orderType} {timeInForce} reason: {reason} for order id: {orderId}",
            domainEvent.TradeType,
            domainEvent.Quantity,
            domainEvent.Symbol,
            domainEvent.OrderType,
            domainEvent.OrderType,
            domainEvent.TimeInForce,
            domainEvent.Reason,
            domainEvent.OrderId);

        return Task.CompletedTask;
    }
}