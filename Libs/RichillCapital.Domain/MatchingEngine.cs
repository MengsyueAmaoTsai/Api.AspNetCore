using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.DataFeeds;

namespace RichillCapital.Domain;

internal sealed class FakeMatchingEngine(
    ILogger<FakeMatchingEngine> _logger,
    IDataFeedManager _dataFeedManager) :
    IMatchingEngine
{
    public void MatchOrder(Order order)
    {
        if (order.Type == OrderType.Market)
        {
            MatchMarketOrder(order);
            return;
        }
    }

    private void MatchMarketOrder(Order order)
    {
        _logger.LogInformation("Matching order {order}", order);

        var entries = GetOrderBook(order.Symbol)
            .GetOppositeEntries(order.TradeType)
            .ToList();

        if (!entries.Any())
        {
            _logger.LogInformation("No matching orders found for market order {orderId}.", order.Id);
            return;
        }

        foreach (var entry in entries)
        {
            if (order.RemainingQuantity <= 0)
            {
                break;
            }

            var matchQuantity = Math.Min(order.RemainingQuantity, entry.Size);
            var executionPrice = entry.Price;

            var executionResult = order.Execute(matchQuantity, executionPrice);

            if (executionResult.IsFailure)
            {
                _logger.LogError(
                    "Failed to execute market order {orderId}: {error}",
                    order.Id,
                    executionResult.Error);

                return;
            }
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
