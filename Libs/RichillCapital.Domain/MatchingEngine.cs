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
            .GetOppositeEntries(order.TradeType)
            .ToList();

        if (!entries.Any())
        {
            _logger.LogInformation("No matching orders found for market order {orderId}.", order.Id);
            return;
        }

        var remainingQuantity = order.Quantity;

        foreach (var entry in entries)
        {
            if (remainingQuantity <= 0)
            {
                break;
            }

            var matchQuantity = Math.Min(remainingQuantity, entry.Size);
            var executionPrice = entry.Price;

            _logger.LogInformation(
                "Matched {matchQuantity} {tradeType} at {executionPrice} for market order {orderId}.",
                matchQuantity,
                order.TradeType.Name,
                executionPrice,
                order.Id);

            // Update order quantity
            remainingQuantity -= matchQuantity;
        }
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
