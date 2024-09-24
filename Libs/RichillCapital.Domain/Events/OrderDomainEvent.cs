using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain.Events;

public abstract record OrderDomainEvent : DomainEvent
{
    public required OrderId OrderId { get; init; }
    public required AccountId AccountId { get; init; }
    public required Symbol Symbol { get; init; }
    public required TradeType TradeType { get; init; }
    public required OrderType OrderType { get; init; }
    public required TimeInForce TimeInForce { get; init; }
    public required decimal Quantity { get; init; }
    public required decimal RemainingQuantity { get; init; }
    public required decimal ExecutedQuantity { get; init; }
    public required OrderStatus Status { get; init; }
    public required string ClientOrderId { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}

public sealed record OrderCreatedDomainEvent : OrderDomainEvent
{
}

public sealed record OrderRejectedDomainEvent : OrderDomainEvent
{
    public required string Reason { get; init; }
}

public sealed record OrderCancelledDomainEvent : OrderDomainEvent
{
}

public sealed record OrderAcceptedDomainEvent : OrderDomainEvent
{
}

public sealed record OrderExecutedDomainEvent : OrderDomainEvent
{
    public required decimal Price { get; init; }
}