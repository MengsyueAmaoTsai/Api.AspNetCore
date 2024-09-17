using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Trade : Entity<TradeId>
{
    private Trade(
        TradeId id,
        AccountId accountId,
        Symbol symbol,
        Side side,
        decimal quantity,
        decimal entryPrice,
        decimal entryTimeUtc,
        decimal exitPrice,
        decimal exitTimeUtc,
        decimal commission,
        decimal tax,
        decimal swap)
        : base(id)
    {
        AccountId = accountId;
        Symbol = symbol;
        Side = side;
        Quantity = quantity;
        EntryPrice = entryPrice;
        EntryTimeUtc = entryTimeUtc;
        ExitPrice = exitPrice;
        ExitTimeUtc = exitTimeUtc;
        Commission = commission;
        Tax = tax;
        Swap = swap;
    }

    public AccountId AccountId { get; private set; }
    public Symbol Symbol { get; private set; }
    public Side Side { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal EntryPrice { get; private set; }
    public decimal EntryTimeUtc { get; private set; }
    public decimal ExitPrice { get; private set; }
    public decimal ExitTimeUtc { get; private set; }
    public decimal Commission { get; private set; }
    public decimal Tax { get; private set; }
    public decimal Swap { get; private set; }

    public static ErrorOr<Trade> Create(
        TradeId id,
        AccountId accountId,
        Symbol symbol,
        Side side,
        decimal quantity,
        decimal entryPrice,
        decimal entryTimeUtc,
        decimal exitPrice,
        decimal exitTimeUtc,
        decimal commission,
        decimal tax,
        decimal swap)
    {
        var trade = new Trade(
            id,
            accountId,
            symbol,
            side,
            quantity,
            entryPrice,
            entryTimeUtc,
            exitPrice,
            exitTimeUtc,
            commission,
            tax,
            swap);

        trade.RegisterDomainEvent(new TradeCreatedDomainEvent
        {
            TradeId = trade.Id,
        });

        return ErrorOr<Trade>.With(trade);
    }
}
