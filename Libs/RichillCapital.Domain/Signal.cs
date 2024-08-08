using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Signal : Entity<SignalId>
{
    private Signal(
        SignalId id,
        SignalSourceId sourceId) : base(id)
    {
        SourceId = sourceId;
    }

    public SignalSourceId SourceId { get; private set; }

    #region Navigation Properties

    public SignalSource Source { get; private set; }

    #endregion

    public static ErrorOr<Signal> Create(
        SignalId id,
        SignalSourceId sourceId)
    {
        var signal = new Signal(
            id,
            sourceId);

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