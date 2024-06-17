using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Signal : Entity<SignalId>
{
    private Signal(
        SignalId id,
        SignalSourceId sourceId,
        DateTimeOffset time,
        string exchange,
        string symbol,
        int latency)
        : base(id)
    {
        SourceId = sourceId;
        Time = time;
        Exchange = exchange;
        Symbol = symbol;
        Latency = latency;
    }

    public SignalSourceId SourceId { get; private set; }

    public DateTimeOffset Time { get; private set; }

    public string Exchange { get; private set; }

    public string Symbol { get; private set; }

    public int Latency { get; private set; }

    public static ErrorOr<Signal> Create(
        SignalId id,
        SignalSourceId sourceId,
        DateTimeOffset time,
        string exchange,
        string symbol,
        int latency)
    {
        if (time == default)
        {
            return Error
                .Invalid("Signals.InvalidTime", "Signal time must be provided")
                .ToErrorOr<Signal>();
        }

        if (latency < 0)
        {
            return Error
                .Invalid("Signals.InvalidLatency", "Signal latency must be greater than or equal to 0")
                .ToErrorOr<Signal>();
        }

        var signal = new Signal(
            id,
            sourceId,
            time,
            exchange,
            symbol,
            latency);

        return signal
            .ToErrorOr();
    }
}