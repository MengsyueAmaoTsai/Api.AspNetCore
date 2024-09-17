using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Events;
using RichillCapital.UseCases.Abstractions;

internal sealed class SignalCreatedDomainEventHandler(
    ILogger<SignalCreatedDomainEventHandler> _logger) :
    IDomainEventHandler<SignalCreatedDomainEvent>
{
    public Task Handle(
        SignalCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "SIGNAL CREATED: [{time}] signal of source [{sourceId}] from origin [{origin}]",
            domainEvent.Time,
            domainEvent.SourceId,
            domainEvent.Origin);

        return Task.CompletedTask;
    }
}