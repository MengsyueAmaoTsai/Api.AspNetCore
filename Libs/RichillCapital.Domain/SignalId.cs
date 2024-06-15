using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class SignalId :
    SingleValueObject<string>
{
    public const int MaxLength = 36;

    private SignalId(string value)
        : base(value)
    {
    }

    public static SignalId NewSignalId() => From(Guid.NewGuid().ToString()).Value;

    public static Result<SignalId> From(string value) => value
        .ToResult()
        .Then(id => new SignalId(id));
}
