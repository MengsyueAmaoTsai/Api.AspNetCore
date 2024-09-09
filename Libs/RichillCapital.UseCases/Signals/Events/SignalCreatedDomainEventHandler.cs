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
            "[{time}] Received signal from origin: {origin}. SourceId: {sourceId}",
            domainEvent.Time,
            domainEvent.Origin,
            domainEvent.SourceId);

        return Task.CompletedTask;
    }
}