using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Signal : Entity<SignalId>
{
    private Signal(SignalId id)
        : base(id)
    {
    }

    public static ErrorOr<Signal> Create(
        SignalId id)
    {
        var signal = new Signal(id);

        return signal
            .ToErrorOr();
    }
}