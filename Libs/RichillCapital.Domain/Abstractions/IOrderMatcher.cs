using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Abstractions;

public interface IOrderMatcher
{
    Result Match(Order order);
}

internal sealed class FakeOrderMatcher(
    IOrderBooks _orderBooks) : IOrderMatcher
{
    public Result Match(Order order)
    {
        if (order.Type == OrderType.Market && order.TimeInForce == TimeInForce.ImmediateOrCancel)
        {
            return MatchMarketOrderImmediateOrCancel(order);
        }

        return Result.Success;
    }

    private Result MatchMarketOrderImmediateOrCancel(
        Order order)
    {
        var oppositeEntries = order.TradeType == TradeType.Buy ?
            _orderBooks.Get(order.Symbol).Asks :
            _orderBooks.Get(order.Symbol).Bids;

        if (!oppositeEntries.Any())
        {
            order.Cancel();

            return Result.Success;
        }

        var entry = oppositeEntries.First();

        var executionQuantity = order.Quantity;
        var executionPrice = entry.Price;

        var executionResult = order.Execute(
            executionQuantity,
            executionPrice);

        if (executionResult.IsFailure)
        {
            return executionResult;
        }

        return Result.Success;
    }
}