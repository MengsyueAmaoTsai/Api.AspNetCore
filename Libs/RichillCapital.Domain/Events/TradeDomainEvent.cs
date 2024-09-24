using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain.Events;

public sealed record TradeCreatedDomainEvent : DomainEvent
{
    public required TradeId TradeId { get; init; }
    public required AccountId AccountId { get; init; }
    public required Symbol Symbol { get; init; }
    public required Side Side { get; init; }
    public required decimal Quantity { get; init; }
    public required decimal EntryPrice { get; init; }
    public required DateTimeOffset EntryTimeUtc { get; init; }
    public required decimal ExitPrice { get; init; }
    public required DateTimeOffset ExitTimeUtc { get; init; }
    public required decimal Commission { get; init; }
    public required decimal Tax { get; init; }
    public required decimal Swap { get; init; }
    public required decimal ProfitLoss { get; init; }
}