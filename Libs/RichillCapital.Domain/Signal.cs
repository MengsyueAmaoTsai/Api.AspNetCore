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
        CreatedTimeUtc = createdTimeUtc;
    }

    public SignalSourceId SourceId { get; init; }
    public SignalOrigin Origin { get; init; }
    public Symbol Symbol { get; init; }
    public DateTimeOffset Time { get; init; }
    public TradeType TradeType { get; init; }
    public decimal Quantity { get; init; }
    public long Latency { get; init; }
    public DateTimeOffset CreatedTimeUtc { get; init; }

    public static ErrorOr<Signal> Create(
        SignalId id,
        SignalSourceId sourceId,
        SignalOrigin origin,
        Symbol symbol,
        DateTimeOffset time,
        TradeType tradeType,
        decimal quantity,
        long latency,
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
            Latency = signal.Latency,
        });

        return ErrorOr<Signal>.With(signal);
    }

    public Result Delay()
    {
        RegisterDomainEvent(new SignalDelayedDomainEvent()
        {
            SignalId = Id,
            SourceId = SourceId,
        });

        return Result.Success;
    }

    public Result Accept()
    {
        RegisterDomainEvent(new SignalAcceptedDomainEvent()
        {
            SignalId = Id,
            SourceId = SourceId,
        });

        return Result.Success;
    }
}