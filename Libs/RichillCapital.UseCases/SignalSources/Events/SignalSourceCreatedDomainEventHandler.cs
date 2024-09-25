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
        _logger.LogInformation(
            "[{EventName}] Signal source {SourceId} has been {Status} at {CreatedTimeUtc}. " +
            "Name: {Name}, Description: {Description} Visibility: {Visibility}.",
            typeof(SignalSourceCreatedDomainEvent).Name,
            domainEvent.SignalSourceId,
            domainEvent.Status,
            domainEvent.CreatedTimeUtc.ToString("yyyy-MM-dd HH:mm:ss"),
            domainEvent.Name,
            domainEvent.Description,
            domainEvent.Visibility);

        return Task.CompletedTask;
    }
}