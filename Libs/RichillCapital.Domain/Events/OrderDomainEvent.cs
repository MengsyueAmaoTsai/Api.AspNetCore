using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain.Events;

public sealed record OrderCreatedDomainEvent : DomainEvent
{
    public required OrderId OrderId { get; init; }
}

public sealed record OrderExecutedDomainEvent : DomainEvent
{
    public required OrderId OrderId { get; init; }
    public required Symbol Symbol { get; init; }
    public required TradeType TradeType { get; init; }
    public required OrderType OrderType { get; init; }
    public required TimeInForce TimeInForce { get; init; }
    public required decimal Quantity { get; init; }
    public required decimal Price { get; init; }
}

public sealed record OrderCancelledDomainEvent : DomainEvent
{
    public required OrderId OrderId { get; init; }
}