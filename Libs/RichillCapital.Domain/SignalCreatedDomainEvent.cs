using RichillCapital.Domain.Common.Events;

namespace RichillCapital.Domain;

public sealed record SignalCreatedDomainEvent : DomainEvent
{
    public required SignalId SignalId { get; init; }
}