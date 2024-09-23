using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Events;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Signals.Events;

internal sealed class SignalDelayedDomainEventHandler(
    ILogger<SignalDelayedDomainEventHandler> _logger) :
    IDomainEventHandler<SignalDelayedDomainEvent>
{
    public Task Handle(
        SignalDelayedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "[SignalDelayed] {signalId} delayed for {delay}ms",
            domainEvent.SourceId,
            domainEvent.Latency);

        return Task.CompletedTask;
    }
}