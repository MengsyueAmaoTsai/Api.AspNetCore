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
            .Then(id => new SignalId(id));

    public static SignalId NewSignalId() =>
        From(Guid.NewGuid().ToString()).ThrowIfFailure().Value;
}