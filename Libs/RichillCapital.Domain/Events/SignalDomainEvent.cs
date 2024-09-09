using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain.Events;

public sealed record SignalCreatedDomainEvent : DomainEvent
{
    public required DateTimeOffset Time { get; init; }
    public required string Origin { get; init; }
    public required string SourceId { get; init; }
}
