using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Signal : Entity<SignalId>
{
    private Signal(
        SignalId id,
        SignalSourceId sourceId,
        DateTimeOffset time,
        int latency)
        : base(id)
    {
        SourceId = sourceId;
        Time = time;
        Latency = latency;
    }

    public SignalSourceId SourceId { get; private set; }

    public DateTimeOffset Time { get; private set; }

    public int Latency { get; private set; }

    public static ErrorOr<Signal> Create(
        SignalId id,
        SignalSourceId sourceId,
        DateTimeOffset time,
        int latency)
    {
        var signal = new Signal(
            id,
            sourceId,
            time,
            latency);

        return signal
            .ToErrorOr();
    }
}