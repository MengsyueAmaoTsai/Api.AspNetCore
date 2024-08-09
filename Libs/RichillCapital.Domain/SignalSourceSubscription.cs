using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class SignalSourceSubscription :
    Entity<SignalSourceSubscriptionId>
{
    private SignalSourceSubscription(
        SignalSourceSubscriptionId id,
        UserId userId,
        SignalSourceId sourceId)
        : base(id)
    {
        UserId = userId;
        SourceId = sourceId;
    }

    public UserId UserId { get; private set; }
    public SignalSourceId SourceId { get; private set; }

    #region Navigation Properties 

    public SignalSource Source { get; private set; }

    #endregion

    public static ErrorOr<SignalSourceSubscription> Create(
        SignalSourceSubscriptionId id,
        UserId userId,
        SignalSourceId sourceId)
    {
        var subscription = new SignalSourceSubscription(
            id,
            userId,
            sourceId);

        return ErrorOr<SignalSourceSubscription>.With(subscription);
    }
}

public sealed class SignalSourceSubscriptionId : SingleValueObject<string>
{
    internal const int MaxLength = 36;

    private SignalSourceSubscriptionId(string value)
        : base(value)
    {
    }

    public static Result<SignalSourceSubscriptionId> From(string value) =>
        Result<string>
            .With(value)
            .Then(id => new SignalSourceSubscriptionId(id));
}