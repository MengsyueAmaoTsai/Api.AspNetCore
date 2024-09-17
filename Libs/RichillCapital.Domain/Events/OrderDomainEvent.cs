using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain.Events;

public sealed record OrderCreatedDomainEvent : DomainEvent
{
    public required OrderId OrderId { get; init; }
}