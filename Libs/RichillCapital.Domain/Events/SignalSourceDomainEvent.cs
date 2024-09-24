using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain.Events;

public abstract record SignalSourceDomainEvent : DomainEvent
{
    public required SignalSourceId SignalSourceId { get; init; }

}

public sealed record SignalSourceCreatedDomainEvent : SignalSourceDomainEvent
{
}