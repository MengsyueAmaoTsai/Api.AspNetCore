using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Events;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Signals.Events;

internal sealed class SignalAcceptedDomainEventHandler(
    ILogger<SignalAcceptedDomainEventHandler> _logger) :
    IDomainEventHandler<SignalAcceptedDomainEvent>
{
    public Task Handle(
        SignalAcceptedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "[SignalAccepted] {signalId}",
            domainEvent.SourceId);

        return Task.CompletedTask;
    }
}