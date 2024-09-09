
using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Events;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Orders.Events;

internal sealed class OrderCreatedDomainEventHandler(
    ILogger<OrderCreatedDomainEventHandler> _logger) :
    IDomainEventHandler<OrderCreatedDomainEvent>
{
    public Task Handle(OrderCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Order with id {OrderId} has been created",
            domainEvent.OrderId);

        return Task.CompletedTask;
    }
}