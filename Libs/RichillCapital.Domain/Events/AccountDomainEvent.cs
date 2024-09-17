using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain.Events;

public sealed record AccountCreatedDomainEvent : DomainEvent
{
    public required AccountId AccountId { get; init; }
}