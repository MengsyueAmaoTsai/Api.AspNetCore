using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain.Events;

public sealed record SignalCreatedDomainEvent : DomainEvent
{
    public required DateTimeOffset Time { get; init; }
    public required SignalSourceId SourceId { get; init; }
    public required SignalOrigin Origin { get; init; }
}
