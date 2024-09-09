using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Abstractions;

public abstract record DomainEvent : IDomainEvent
{
    public DateTimeOffset OccurredTime => DateTimeOffset.UtcNow;
}