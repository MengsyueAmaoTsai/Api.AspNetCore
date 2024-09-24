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
        DateTimeOffset entryTimeUtc,
        decimal exitPrice,
        DateTimeOffset exitTimeUtc,
        decimal commission,
        decimal tax,
        decimal swap,
        decimal profitLoss)
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
        ProfitLoss = profitLoss;
    }

    public AccountId AccountId { get; private set; }
    public Symbol Symbol { get; private set; }
    public Side Side { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal EntryPrice { get; private set; }
    public DateTimeOffset EntryTimeUtc { get; private set; }
    public decimal ExitPrice { get; private set; }
    public DateTimeOffset ExitTimeUtc { get; private set; }
    public decimal Commission { get; private set; }
    public decimal Tax { get; private set; }
    public decimal Swap { get; private set; }
    public decimal ProfitLoss { get; private set; }

    public static ErrorOr<Trade> Create(
        TradeId id,
        AccountId accountId,
        Symbol symbol,
        Side side,
        decimal quantity,
        decimal entryPrice,
        DateTimeOffset entryTimeUtc,
        decimal exitPrice,
        DateTimeOffset exitTimeUtc,
        decimal commission,
        decimal tax,
        decimal swap,
        decimal profitLoss)
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
            swap,
            profitLoss);

        trade.RegisterDomainEvent(new TradeCreatedDomainEvent
        {
            TradeId = id,
            AccountId = accountId,
            Symbol = symbol,
            Side = side,
            Quantity = quantity,
            EntryPrice = entryPrice,
            EntryTimeUtc = entryTimeUtc,
            ExitPrice = exitPrice,
            ExitTimeUtc = exitTimeUtc,
            Commission = commission,
            Tax = tax,
            Swap = swap,
            ProfitLoss = profitLoss,
        });

        return ErrorOr<Trade>.With(trade);
    }
}
