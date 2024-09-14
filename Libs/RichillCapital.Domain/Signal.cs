using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Signal : Entity<SignalId>
{
    private Signal(
        SignalId id,
        SignalSourceId sourceId,
        DateTimeOffset time,
        SignalOrigin origin,
        DateTimeOffset createdTimeUtc)
        : base(id)
    {
        SourceId = sourceId;
        Time = time;
        Origin = origin;
        CreatedTimeUtc = createdTimeUtc;
    }

    public SignalSourceId SourceId { get; init; }
    public DateTimeOffset Time { get; init; }
    public SignalOrigin Origin { get; init; }
    public DateTimeOffset CreatedTimeUtc { get; init; }

    public static ErrorOr<Signal> Create(
        SignalId id,
        SignalSourceId sourceId,
        DateTimeOffset time,
        SignalOrigin origin,
        DateTimeOffset createdTimeUtc)
    {
        var signal = new Signal(
            id,
            sourceId,
            time,
            origin,
            createdTimeUtc);

        signal.RegisterDomainEvent(new SignalCreatedDomainEvent
        {
            Time = time,
            SourceId = sourceId,
            Origin = origin,
        });

        return ErrorOr<Signal>.With(signal);
    }
}