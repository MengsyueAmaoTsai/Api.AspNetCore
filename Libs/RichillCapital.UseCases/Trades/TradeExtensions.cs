using RichillCapital.Domain;

namespace RichillCapital.UseCases.Trades;

internal static class TradeExtensions
{
    internal static TradeDto ToDto(this Trade trade) =>
        new()
        {
            Id = trade.Id.Value,
            Symbol = trade.Symbol.Value,
            Side = trade.Side.Name,
        };
}