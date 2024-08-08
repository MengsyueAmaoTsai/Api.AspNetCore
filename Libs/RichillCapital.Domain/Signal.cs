using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Signal : Entity<SignalId>
{
    private Signal(SignalId id) : base(id)
    {
    }

    public static ErrorOr<Signal> Create(SignalId id)
    {
        var signal = new Signal(id);

        return ErrorOr<Signal>.With(signal);
    }
}

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
}