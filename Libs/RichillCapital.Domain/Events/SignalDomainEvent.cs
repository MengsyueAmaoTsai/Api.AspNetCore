using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain.Events;

public abstract record SignalDomainEvent : DomainEvent
{
    public required SignalId SignalId { get; init; }
    public required SignalSourceId SourceId { get; init; }
    public required SignalOrigin Origin { get; init; }
    public required Symbol Symbol { get; init; }
    public required DateTimeOffset Time { get; init; }
    public required TradeType TradeType { get; init; }
    public required decimal Quantity { get; init; }
    public required long Latency { get; init; }
    public required SignalStatus Status { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}

public sealed record SignalCreatedDomainEvent : SignalDomainEvent
{
}

public sealed record SignalDelayedDomainEvent : SignalDomainEvent
{
}

public sealed record SignalEmittedDomainEvent : SignalDomainEvent
{
}

public sealed record SignalBlockedDomainEvent : SignalDomainEvent
{
}