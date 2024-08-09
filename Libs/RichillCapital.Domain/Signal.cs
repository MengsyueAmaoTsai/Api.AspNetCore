using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Signal : Entity<SignalId>
{
    private Signal(
        SignalId id,
        SignalSourceId sourceId,
        DateTimeOffset time,
        Symbol symbol) : base(id)
    {
        SourceId = sourceId;
        Time = time;
        Symbol = symbol;
    }

    public SignalSourceId SourceId { get; private set; }

    public DateTimeOffset Time { get; private set; }

    public Symbol Symbol { get; private set; }

    #region Navigation Properties

    public SignalSource Source { get; private set; }

    #endregion

    public static ErrorOr<Signal> Create(
        SignalId id,
        SignalSourceId sourceId,
        DateTimeOffset time,
        Symbol symbol)
    {
        var signal = new Signal(
            id,
            sourceId,
            time,
            symbol);

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

public sealed class Symbol : SingleValueObject<string>
{
    internal const int MaxLength = 10;

    private Symbol(string value)
        : base(value)
    {
    }

    public static Result<Symbol> From(string value) =>
        Result<string>
            .With(value)
            .Ensure(symbol => !string.IsNullOrEmpty(symbol), Error.Invalid("Symbol cannot be empty."))
            .Ensure(symbol => symbol.Length <= MaxLength, Error.Invalid($"Symbol cannot be longer than {MaxLength} characters."))
            .Then(symbol => new Symbol(symbol));
}