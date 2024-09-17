using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain.Events;

public sealed record ExecutionCreatedDomainEvent : DomainEvent
{
    public required ExecutionId ExecutionId { get; init; }
}