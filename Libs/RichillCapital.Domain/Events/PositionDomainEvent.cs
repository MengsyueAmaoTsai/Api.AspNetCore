using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain.Events;

public sealed record PositionCreatedDomainEvent : DomainEvent
{
    public required PositionId PositionId { get; init; }
}