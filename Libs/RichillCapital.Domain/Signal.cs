using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Signal : Entity<SignalId>
{
    private Signal(
        SignalId id,
        SignalSourceId sourceId,
        DateTimeOffset time) : base(id)
    {
        SourceId = sourceId;
        Time = time;
    }

    public SignalSourceId SourceId { get; private set; }

    public DateTimeOffset Time { get; private set; }

    #region Navigation Properties

    public SignalSource Source { get; private set; }

    #endregion

    public static ErrorOr<Signal> Create(
        SignalId id,
        SignalSourceId sourceId,
        DateTimeOffset time)
    {
        var signal = new Signal(
            id,
            sourceId,
            time);

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

    public static SignalId NewSignalId() =>
        From(Guid.NewGuid().ToString()).ThrowIfFailure().ValueOrDefault;
}