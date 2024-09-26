using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Events;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Signals.Events;

internal sealed class SignalBlockedDomainEventHandler(
    ILogger<SignalBlockedDomainEventHandler> _logger) :
    IDomainEventHandler<SignalBlockedDomainEvent>
{
    public Task Handle(
        SignalBlockedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogSignalDomainEvent(domainEvent);

        return Task.CompletedTask;
    }
}