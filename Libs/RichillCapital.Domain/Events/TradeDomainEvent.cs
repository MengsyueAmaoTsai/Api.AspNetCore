using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain.Events;

public sealed record TradeCreatedDomainEvent : DomainEvent
{
    public required TradeId TradeId { get; init; }
}