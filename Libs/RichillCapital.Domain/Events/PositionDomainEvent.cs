using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain.Events;

public sealed record PositionCreatedDomainEvent : DomainEvent
{
    public required PositionId PositionId { get; init; }
    public required Symbol Symbol { get; init; }
    public required Side Side { get; init; }
    public required decimal Quantity { get; init; }
    public required decimal AveragePrice { get; init; }
}

public sealed record PositionUpdatedDomainEvent : DomainEvent
{
    public required PositionId PositionId { get; init; }
    public required Symbol Symbol { get; init; }
    public required Side Side { get; init; }
    public required decimal Quantity { get; init; }
    public required decimal AveragePrice { get; init; }
}


public sealed record PositionClosedDomainEvent : DomainEvent
{
    public required PositionId PositionId { get; init; }
}