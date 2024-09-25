using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain.Events;

public abstract record SignalSourceDomainEvent : DomainEvent
{
    public required SignalSourceId SignalSourceId { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required SignalSourceVisibility Visibility { get; init; }
    public required SignalSourceStatus Status { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}

public sealed record SignalSourceCreatedDomainEvent : SignalSourceDomainEvent
{
}