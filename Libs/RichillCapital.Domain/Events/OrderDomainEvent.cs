using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain.Events;

public sealed record OrderCreatedDomainEvent : DomainEvent
{
    public required OrderId OrderId { get; init; }
}

public sealed record OrderExecutedDomainEvent : DomainEvent
{
    public required OrderId OrderId { get; init; }
}

public sealed record OrderCancelledDomainEvent : DomainEvent
{
    public required OrderId OrderId { get; init; }
}