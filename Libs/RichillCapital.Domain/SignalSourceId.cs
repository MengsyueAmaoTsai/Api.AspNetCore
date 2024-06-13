using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class SignalSourceId :
    SingleValueObject<string>
{
    public const int MaxLength = 36;

    private SignalSourceId(string value)
        : base(value)
    {
    }

    public static Result<SignalSourceId> From(string value) => value
        .ToResult()
        .Then(id => new SignalSourceId(id));
}