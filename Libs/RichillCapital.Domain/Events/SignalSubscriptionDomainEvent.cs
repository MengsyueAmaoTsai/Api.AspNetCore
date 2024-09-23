using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain.Events;

public abstract record SignalSubscriptionDomainEvent : DomainEvent
{
    public required SignalSubscriptionId SubscriptionId { get; init; }
}

public sealed record SignalSubscriptionCreatedDomainEvent :
    SignalSubscriptionDomainEvent
{
}