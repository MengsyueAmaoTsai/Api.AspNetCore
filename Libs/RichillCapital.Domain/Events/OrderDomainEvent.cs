using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain.Events;

public sealed record OrderCreatedDomainEvent : DomainEvent
{
    public required OrderId OrderId { get; init; }
    public required AccountId AccountId { get; init; }
    public required Symbol Symbol { get; init; }
    public required TradeType TradeType { get; init; }
    public required OrderType OrderType { get; init; }
    public required TimeInForce TimeInForce { get; init; }
    public required decimal Quantity { get; init; }
    public required OrderStatus Status { get; init; }
}

public sealed record OrderRejectedDomainEvent : DomainEvent
{
    public required OrderId OrderId { get; init; }
    public required AccountId AccountId { get; init; }
    public required Symbol Symbol { get; init; }
    public required TradeType TradeType { get; init; }
    public required OrderType OrderType { get; init; }
    public required TimeInForce TimeInForce { get; init; }
    public required decimal Quantity { get; init; }
    public required OrderStatus Status { get; init; }
    public required string Reason { get; init; }
}

public sealed record OrderCancelledDomainEvent : DomainEvent
{
    public required OrderId OrderId { get; init; }
}

public sealed record OrderAcceptedDomainEvent : DomainEvent
{
    public required OrderId OrderId { get; init; }
    public required AccountId AccountId { get; init; }
    public required Symbol Symbol { get; init; }
    public required TradeType TradeType { get; init; }
    public required OrderType OrderType { get; init; }
    public required TimeInForce TimeInForce { get; init; }
    public required decimal Quantity { get; init; }
    public required OrderStatus Status { get; init; }
}

public sealed record OrderExecutedDomainEvent : DomainEvent
{
    public required AccountId AccountId { get; init; }
    public required OrderId OrderId { get; init; }
    public required Symbol Symbol { get; init; }
    public required TradeType TradeType { get; init; }
    public required OrderType OrderType { get; init; }
    public required TimeInForce TimeInForce { get; init; }
    public required decimal Quantity { get; init; }
    public required decimal Price { get; init; }
}