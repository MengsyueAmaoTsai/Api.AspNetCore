using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Events;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Positions.Events;

internal sealed class PositionUpdatedDomainEventHandler(
    ILogger<PositionUpdatedDomainEventHandler> _logger) :
    IDomainEventHandler<PositionUpdatedDomainEvent>
{
    public Task Handle(
        PositionUpdatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("POSITION UPDATED: {id}", domainEvent.PositionId);

        return Task.CompletedTask;
    }
}