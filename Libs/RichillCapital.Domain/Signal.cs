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
        Symbol symbol,
        TradeType tradeType,
        OrderType orderType,
        decimal quantity,
        DateTimeOffset createdTimeUtc)
        : base(id)
    {
        SourceId = sourceId;
        Time = time;
        Origin = origin;
        Symbol = symbol;
        TradeType = tradeType;
        OrderType = orderType;
        Quantity = quantity;
        CreatedTimeUtc = createdTimeUtc;
    }

    public SignalSourceId SourceId { get; init; }
    public DateTimeOffset Time { get; init; }
    public SignalOrigin Origin { get; init; }
    public Symbol Symbol { get; init; }
    public TradeType TradeType { get; init; }
    public OrderType OrderType { get; init; }
    public decimal Quantity { get; init; }
    public DateTimeOffset CreatedTimeUtc { get; init; }

    public static ErrorOr<Signal> Create(
        SignalId id,
        SignalSourceId sourceId,
        DateTimeOffset time,
        SignalOrigin origin,
        Symbol symbol,
        TradeType tradeType,
        OrderType orderType,
        decimal quantity,
        DateTimeOffset createdTimeUtc)
    {
        var signal = new Signal(
            id,
            sourceId,
            time,
            origin,
            symbol,
            tradeType,
            orderType,
            quantity,
            createdTimeUtc);

        signal.RegisterDomainEvent(new SignalCreatedDomainEvent
        {
            Time = time,
            SourceId = sourceId,
            Origin = origin,
            Symbol = symbol,
            TradeType = tradeType,
            OrderType = orderType,
            Quantity = quantity,
        });

        return ErrorOr<Signal>.With(signal);
    }
}