using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class SignalId : SingleValueObject<string>
{
    internal const int MaxLength = 36;

    private SignalId(string value)
        : base(value)
    {
    }

    public static Result<SignalId> From(string value) =>
        Result<string>
            .With(value)
            .Ensure(id => !string.IsNullOrEmpty(id), Error.Invalid($"{nameof(SignalId)} cannot be empty"))
            .Ensure(id => id.Length <= MaxLength, Error.Invalid($"{nameof(SignalId)} cannot be longer than {MaxLength} characters"))
            .Then(id => new SignalId(id));

    public static SignalId NewSignalId() =>
        From(Guid.NewGuid().ToString()).ThrowIfFailure().Value;
}