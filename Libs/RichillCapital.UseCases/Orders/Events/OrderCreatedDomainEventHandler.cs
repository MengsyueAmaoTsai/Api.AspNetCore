using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Events;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Orders.Events;

internal sealed class OrderCreatedDomainEventHandler(
    ILogger<OrderCreatedDomainEventHandler> _logger) :
    IDomainEventHandler<OrderCreatedDomainEvent>
{
    public Task Handle(
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

        return Task.CompletedTask;
    }
}