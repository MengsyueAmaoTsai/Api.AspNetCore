using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain.Events;

public sealed record ExecutionCreatedDomainEvent : DomainEvent
{
    public required ExecutionId ExecutionId { get; init; }
    public required Symbol Symbol { get; init; }
    public required TradeType TradeType { get; init; }
    public required OrderType OrderType { get; init; }
    public required TimeInForce TimeInForce { get; init; }
    public required decimal Quantity { get; init; }
    public required decimal Price { get; init; }
}