using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Signal : Entity<SignalId>
{
    private Signal(
        SignalId id,
        DateTimeOffset time,
        SignalSourceId sourceId,
        string origin,
        DateTimeOffset createdTimeUtc)
        : base(id)
    {
        Time = time;
        SourceId = sourceId;
        Origin = origin;
        CreatedTimeUtc = createdTimeUtc;
    }

    public DateTimeOffset Time { get; init; }
    public SignalSourceId SourceId { get; init; }
    public string Origin { get; init; }
    public DateTimeOffset CreatedTimeUtc { get; init; }

    public static ErrorOr<Signal> Create(
        SignalId id,
        DateTimeOffset time,
        SignalSourceId sourceId,
        string origin,
        DateTimeOffset createdTimeUtc)
    {
        var signal = new Signal(
            id,
            time,
            sourceId,
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