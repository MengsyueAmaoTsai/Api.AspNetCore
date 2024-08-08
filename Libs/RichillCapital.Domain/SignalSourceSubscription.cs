using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class SignalSourceSubscription :
    Entity<SignalSourceSubscriptionId>
{
    private SignalSourceSubscription(
        SignalSourceSubscriptionId id)
        : base(id)
    {
    }

    public static ErrorOr<SignalSourceSubscription> Create(
        SignalSourceSubscriptionId id)
    {
        var subscription = new SignalSourceSubscription(id);

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