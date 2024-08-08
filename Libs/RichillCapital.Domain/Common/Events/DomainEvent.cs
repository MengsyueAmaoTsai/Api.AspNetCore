using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Common.Events;

public abstract record DomainEvent : IDomainEvent
{
    public DateTimeOffset OccurredTime => DateTimeOffset.UtcNow;
}