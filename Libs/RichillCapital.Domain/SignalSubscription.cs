using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class SignalSubscription : Entity<SignalSubscriptionId>
{
    private SignalSubscription(
        SignalSubscriptionId id,
        UserId userId,
        SignalSourceId sourceId,
        DateTimeOffset createdTimeUtc)
        : base(id)
    {
        UserId = userId;
        SourceId = sourceId;
        CreatedTimeUtc = createdTimeUtc;
    }

    public UserId UserId { get; private set; }
    public SignalSourceId SourceId { get; private set; }
    public DateTimeOffset CreatedTimeUtc { get; private set; }

    public static ErrorOr<SignalSubscription> Create(
        SignalSubscriptionId id,
        UserId userId,
        SignalSourceId sourceId,
        DateTimeOffset createdTimeUtc)
    {
        var subscription = new SignalSubscription(
            id,
            userId,
            sourceId,
            createdTimeUtc);

        subscription.RegisterDomainEvent(new SignalSubscriptionCreatedDomainEvent
        {
            SubscriptionId = subscription.Id,
        });

        return ErrorOr<SignalSubscription>.With(subscription);
    }
}
