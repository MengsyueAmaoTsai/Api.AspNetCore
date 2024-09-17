using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class SignalSourceId : SingleValueObject<string>
{
    internal const int MaxLength = 36;

    private SignalSourceId(string value)
        : base(value)
    {
    }

    public static Result<SignalSourceId> From(string value) =>
        Result<string>
            .With(value)
            .Ensure(id => !string.IsNullOrEmpty(id), Error.Invalid($"{nameof(SignalSourceId)} cannot be empty"))
            .Ensure(id => id.Length <= MaxLength, Error.Invalid($"{nameof(SignalSourceId)} cannot be longer than {MaxLength} characters"))
            .Then(id => new SignalSourceId(id));
}