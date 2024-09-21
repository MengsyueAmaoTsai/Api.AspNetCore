using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain.Events;

public sealed record SignalCreatedDomainEvent : DomainEvent
{
    public required DateTimeOffset Time { get; init; }
    public required SignalSourceId SourceId { get; init; }
    public required SignalOrigin Origin { get; init; }
    public required Symbol Symbol { get; init; }
    public required TradeType TradeType { get; init; }
    public required OrderType OrderType { get; init; }
    public required decimal Quantity { get; init; }
}
