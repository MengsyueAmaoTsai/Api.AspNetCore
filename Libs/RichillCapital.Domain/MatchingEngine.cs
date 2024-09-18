using Microsoft.Extensions.Logging;

namespace RichillCapital.Domain;

public interface IMatchingEngine
{
    void Match(Order order);
}

internal sealed class FakeMatchingEngine(
    ILogger<FakeMatchingEngine> _logger) :
    IMatchingEngine
{
    public void Match(Order order)
    {
        _logger.LogInformation("Matching order {orderId}...", order.Id);

        if (order.Type == OrderType.Market)
        {
            MatchMarketOrder(order);
            return;
        }

    }

    private void MatchMarketOrder(Order order)
    {
        _logger.LogInformation("Matching market order {orderId}...", order.Id);

        var entries = GetOrderBook(order.Symbol)
            .GetOppositeEntries(order.TradeType);

        var executionQuantity = order.Quantity;
        var executionPrice = entries.First().Price;

        _logger.LogInformation(
            "Matched market order {orderId} with {executionQuantity} @ {executionPrice}",
            order.Id,
            executionQuantity,
            executionPrice);
    }

    private static OrderBook GetOrderBook(
        Symbol symbol)
    {
        return new OrderBook
        {
            Symbol = symbol,
            Bids =
            [
                (Size: 1.0m, Price: 100.0m),
                (Size: 2.0m, Price: 99.0m),
                (Size: 3.0m, Price: 98.0m),
            ],
            Asks =
            [
                (Size: 1.0m, Price: 101.0m),
                (Size: 2.0m, Price: 102.0m),
                (Size: 3.0m, Price: 103.0m),
            ],
        };
    }
}

public sealed record OrderBook
{
    public required Symbol Symbol { get; init; }
    public required IEnumerable<(decimal Size, decimal Price)> Bids { get; init; }
    public required IEnumerable<(decimal Size, decimal Price)> Asks { get; init; }

    public IEnumerable<(decimal Size, decimal Price)> GetOppositeEntries(
        TradeType tradeType)
    {
        return tradeType switch
        {
            { Name: nameof(TradeType.Buy) } => Asks,
            { Name: nameof(TradeType.Sell) } => Bids,
            _ => throw new ArgumentOutOfRangeException(nameof(tradeType)),
        };
    }
}
