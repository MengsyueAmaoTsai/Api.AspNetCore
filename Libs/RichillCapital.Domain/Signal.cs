using RichillCapital.Domain.Errors;
using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Signal : Entity<SignalId>
{
    public const int MaxLatencyInMilliseconds = 1000 * 15;

    private Signal(
        SignalId id,
        SignalSourceId sourceId,
        SignalOrigin origin,
        Symbol symbol,
        DateTimeOffset time,
        TradeType tradeType,
        decimal quantity,
        long latency,
        SignalStatus status,
        DateTimeOffset createdTimeUtc)
        : base(id)
    {
        SourceId = sourceId;
        Origin = origin;
        Symbol = symbol;
        Time = time;
        TradeType = tradeType;
        Quantity = quantity;
        Latency = latency;
        Status = status;
        CreatedTimeUtc = createdTimeUtc;
    }

    public SignalSourceId SourceId { get; private set; }
    public SignalOrigin Origin { get; private set; }
    public Symbol Symbol { get; private set; }
    public DateTimeOffset Time { get; private set; }
    public TradeType TradeType { get; private set; }
    public decimal Quantity { get; private set; }
    public long Latency { get; private set; }
    public SignalStatus Status { get; private set; }
    public DateTimeOffset CreatedTimeUtc { get; private set; }

    public SignalSource Source { get; private set; }

    public static ErrorOr<Signal> Create(
        SignalId id,
        SignalSourceId sourceId,
        SignalOrigin origin,
        Symbol symbol,
        DateTimeOffset time,
        TradeType tradeType,
        decimal quantity,
        long latency,
        SignalStatus status,
        DateTimeOffset createdTimeUtc)
    {
        if (time == default)
        {
            return ErrorOr<Signal>.WithError(SignalErrors.InvalidTime(time));
        }

        if (time > createdTimeUtc)
        {
            return ErrorOr<Signal>.WithError(SignalErrors.TimeInFuture);
        }

        if (quantity <= 0)
        {
            return ErrorOr<Signal>.WithError(SignalErrors.InvalidQuantity(quantity));
        }

        if (latency < 0)
        {
            return ErrorOr<Signal>.WithError(SignalErrors.InvalidLatency(latency));
        }

        var signal = new Signal(
            id,
            sourceId,
            origin,
            symbol,
            time,
            tradeType,
            quantity,
            latency,
            status,
            createdTimeUtc);

        signal.RegisterDomainEvent(new SignalCreatedDomainEvent
        {
            SignalId = id,
            SourceId = sourceId,
            Origin = origin,
            Symbol = symbol,
            Time = time,
            TradeType = tradeType,
            Quantity = quantity,
            Latency = latency,
            Status = status,
            CreatedTimeUtc = createdTimeUtc,
        });

        return ErrorOr<Signal>.With(signal);
    }

    public Result Delay()
    {
        Status = SignalStatus.Delayed;
        RegisterDomainEvent(new SignalDelayedDomainEvent
        {
            SignalId = Id,
            SourceId = SourceId,
            Origin = Origin,
            Symbol = Symbol,
            Time = Time,
            TradeType = TradeType,
            Quantity = Quantity,
            Latency = Latency,
            Status = Status,
            CreatedTimeUtc = CreatedTimeUtc,
        });

        return Result.Success;
    }

    public Result Emit()
    {
        if (Source.Status == SignalSourceStatus.Draft || Source.Status == SignalSourceStatus.Deprecated)
        {
            return Result.Failure(Error.Conflict($"Cannot emit signal from source in {Source.Status} status"));
        }

        Status = SignalStatus.Emitted;
        RegisterDomainEvent(new SignalEmittedDomainEvent
        {
            SignalId = Id,
            SourceId = SourceId,
            Origin = Origin,
            Symbol = Symbol,
            Time = Time,
            TradeType = TradeType,
            Quantity = Quantity,
            Latency = Latency,
            Status = Status,
            CreatedTimeUtc = CreatedTimeUtc,
        });

        return Result.Success;
    }

    public Result Block()
    {
        Status = SignalStatus.Blocked;

        RegisterDomainEvent(new SignalBlockedDomainEvent
        {
            SignalId = Id,
            SourceId = SourceId,
            Origin = Origin,
            Symbol = Symbol,
            Time = Time,
            TradeType = TradeType,
            Quantity = Quantity,
            Latency = Latency,
            Status = Status,
            CreatedTimeUtc = CreatedTimeUtc,
        });

        return Result.Success;
    }
}
