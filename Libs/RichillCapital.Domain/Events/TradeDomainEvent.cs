using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain.Events;

public sealed record TradeCreatedDomainEvent : DomainEvent
{
    public required TradeId TradeId { get; init; }
    public required AccountId AccountId { get; init; }
    public required Symbol Symbol { get; init; }
    public required Side Side { get; init; }
    public required decimal Quantity { get; init; }
}