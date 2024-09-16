using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Trade : Entity<TradeId>
{
    private Trade(
        TradeId id,
        Symbol symbol,
        PositionSide side,
        DateTimeOffset entryTimeUtc,
        decimal entryPrice,
        DateTimeOffset exitTimeUtc,
        decimal exitPrice,
        decimal quantity)
        : base(id)
    {
        Symbol = symbol;
        Side = side;
        EntryPrice = entryPrice;
        EntryTimeUtc = entryTimeUtc;
        ExitPrice = exitPrice;
        ExitTimeUtc = exitTimeUtc;
        Quantity = quantity;
    }

    public Symbol Symbol { get; private set; }
    public PositionSide Side { get; private set; }
    public decimal EntryPrice { get; private set; }
    public DateTimeOffset EntryTimeUtc { get; private set; }
    public decimal ExitPrice { get; private set; }
    public DateTimeOffset ExitTimeUtc { get; private set; }
    public decimal Quantity { get; private set; }

    public static ErrorOr<Trade> Create(
        TradeId id,
        Symbol symbol,
        PositionSide side,
        DateTimeOffset entryTimeUtc,
        decimal entryPrice,
        DateTimeOffset exitTimeUtc,
        decimal exitPrice,
        decimal quantity)
    {
        var trade = new Trade(
            id,
            symbol,
            side,
            entryTimeUtc,
            entryPrice,
            exitTimeUtc,
            exitPrice,
            quantity);

        return ErrorOr<Trade>.With(trade);
    }
}
