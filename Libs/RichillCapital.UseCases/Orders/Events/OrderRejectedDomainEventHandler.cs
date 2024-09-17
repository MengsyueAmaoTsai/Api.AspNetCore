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
        _logger.LogInformation(
            "ORDER REJECTED: {reason} Id: {orderId}",
            domainEvent.Reason,
            domainEvent.OrderId);

        return Task.CompletedTask;
    }
}