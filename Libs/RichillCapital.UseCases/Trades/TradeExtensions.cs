using RichillCapital.Domain;

namespace RichillCapital.UseCases.Trades;

internal static class TradeExtensions
{
    public static TradeDto ToDto(this Trade trade) =>
        new()
        {
            Id = trade.Id.Value,
            AccountId = trade.AccountId.Value,
            Symbol = trade.Symbol.Value,
            Side = trade.Side.ToString(),
            Quantity = trade.Quantity,
            EntryPrice = trade.EntryPrice,
            EntryTimeUtc = trade.EntryTimeUtc,
            ExitPrice = trade.ExitPrice,
            ExitTimeUtc = trade.ExitTimeUtc,
            Commission = trade.Commission,
            Tax = trade.Tax,
            Swap = trade.Swap,
            ProfitLoss = trade.ProfitLoss,
        };
}