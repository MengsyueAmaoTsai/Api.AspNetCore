using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Events;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Orders.Events;

internal sealed class OrderCancelledDomainEventHandler(
    ILogger<OrderCancelledDomainEventHandler> _logger) :
    IDomainEventHandler<OrderCancelledDomainEvent>
{
    public Task Handle(
        OrderCancelledDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogOrderDomainEvent(domainEvent);

        return Task.CompletedTask;
    }
}