using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class SignalSubscriptionId :
    SingleValueObject<string>
{
    internal const int MaxLength = 36;

    private SignalSubscriptionId(string value)
        : base(value)
    {
    }

    public static Result<SignalSubscriptionId> From(string value) =>
        Result<string>
            .With(value)
            .Ensure(
                id => !string.IsNullOrEmpty(id),
                Error.Invalid($"'{nameof(SignalSubscriptionId)}' cannot be empty."))
            .Ensure(
                id => id.Length <= MaxLength,
                Error.Invalid($"'{nameof(SignalSubscriptionId)}' cannot be longer than {MaxLength} characters."))
            .Then(id => new SignalSubscriptionId(id));
}