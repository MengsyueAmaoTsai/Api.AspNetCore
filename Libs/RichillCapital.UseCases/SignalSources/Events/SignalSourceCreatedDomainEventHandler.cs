using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Events;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.SignalSources.Events;

public sealed record SignalSourceCreatedDomainEventHandler(
    ILogger<SignalSourceCreatedDomainEventHandler> _logger) :
    IDomainEventHandler<SignalSourceCreatedDomainEvent>
{
    public Task Handle(
        SignalSourceCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("[SignalSourceCreated] {id}", domainEvent.SignalSourceId);

        return Task.CompletedTask;
    }
}