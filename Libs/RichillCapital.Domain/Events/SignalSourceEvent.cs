using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain.Events;

public sealed record SignalSourceCreatedDomainEvent : DomainEvent
{
    public required SignalSourceId SignalSourceId { get; init; }
}