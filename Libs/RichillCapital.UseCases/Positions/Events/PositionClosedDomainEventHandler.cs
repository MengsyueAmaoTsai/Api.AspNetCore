using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Events;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Positions.Events;

internal sealed class PositionClosedDomainEventHandler(
    ILogger<PositionClosedDomainEventHandler> _logger) :
    IDomainEventHandler<PositionClosedDomainEvent>
{
    public Task Handle(
        PositionClosedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Position with id {PositionId} has been closed",
            domainEvent.PositionId.Value);

        return Task.CompletedTask;
    }
}