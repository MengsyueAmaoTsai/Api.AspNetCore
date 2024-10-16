using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain.Events;

public sealed record ExecutionCreatedDomainEvent : DomainEvent
{
    public required ExecutionId ExecutionId { get; init; }
    public required AccountId AccountId { get; init; }
    public required OrderId OrderId { get; init; }
    public required PositionId PositionId { get; init; }
    public required Symbol Symbol { get; init; }
    public required TradeType TradeType { get; init; }
    public required OrderType OrderType { get; init; }
    public required TimeInForce TimeInForce { get; init; }
    public required decimal Quantity { get; init; }
    public required decimal Price { get; init; }
    public required decimal Commission { get; init; }
    public required decimal Tax { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}