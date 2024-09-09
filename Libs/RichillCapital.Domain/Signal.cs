using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Signal : Entity<SignalId>
{
    private Signal(
        SignalId id,
        DateTimeOffset time,
        string origin,
        string sourceId,
        DateTimeOffset createdTimeUtc)
        : base(id)
    {
        Time = time;
        Origin = origin;
        SourceId = sourceId;
        CreatedTimeUtc = createdTimeUtc;
    }

    public DateTimeOffset Time { get; init; }
    public string Origin { get; init; }
    public string SourceId { get; init; }
    public DateTimeOffset CreatedTimeUtc { get; init; }

    public static ErrorOr<Signal> Create(
        SignalId id,
        DateTimeOffset time,
        string origin,
        string sourceId,
        DateTimeOffset createdTimeUtc)
    {
        var signal = new Signal(
            id,
            time,
            origin,
            sourceId,
            createdTimeUtc);

        signal.RegisterDomainEvent(new SignalCreatedDomainEvent
        {
            Time = time,
            Origin = origin,
            SourceId = sourceId,
        });

        return ErrorOr<Signal>.With(signal);
    }
}