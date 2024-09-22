using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Brokerages;
using RichillCapital.Max;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Brokerages.Max;

internal sealed class MaxBrokerage(
    ILogger<MaxBrokerage> _logger,
    IMaxRestClient _restClient,
    string name) :
    Brokerage("Max", name)
{
    private readonly MaxSymbolMapper _symbolMapper = new();

    public override async Task<Result> StartAsync(CancellationToken cancellationToken = default)
    {
        // Check server time
        var serverTimeResult = await _restClient.GetServerTimeAsync(cancellationToken);

        if (serverTimeResult.IsFailure)
        {
            return Result.Failure(serverTimeResult.Error);
        }

        _logger.LogInformation(
            "Server time: {time}",
            serverTimeResult.Value);

        // User info
        var userInfo = await _restClient.GetUserInfoAsync(cancellationToken);

        if (userInfo.IsFailure)
        {
            return Result.Failure(userInfo.Error);
        }

        _logger.LogInformation(
            "User info: {info}",
            userInfo.Value);

        Status = ConnectionStatus.Active;

        return await OnStartedAsync(cancellationToken);
    }

    public override Task<Result> StopAsync(CancellationToken cancellationToken = default)
    {
        Status = ConnectionStatus.Stopped;

        return Task.FromResult(Result.Success);
    }

    public override async Task<Result> SubmitOrderAsync(
        Symbol symbol,
        TradeType tradeType,
        OrderType orderType,
        TimeInForce timeInForce,
        decimal quantity,
        string clientOrderId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Submitting order: {TradeType} {Symbol} {Quantity} @ {OrderType} {timeInForce} with client order ID {ClientOrderId}",
            tradeType,
            symbol,
            quantity,
            orderType,
            timeInForce,
            clientOrderId);

        var submitResult = await _restClient.SubmitOrderAsync(
            walletType: "spot",
            market: _symbolMapper.ToExternalSymbol(symbol),
            side: tradeType.Name.ToLowerInvariant(),
            volume: quantity,
            price: 0,
            cancellationToken);

        if (submitResult.IsFailure)
        {
            return Result.Failure(submitResult.Error);
        }

        return Result.Success;
    }

    public override async Task<Result<IReadOnlyCollection<Order>>> ListOrdersAsync(CancellationToken cancellationToken = default)
    {
        var maxOrdersResult = await _restClient.ListOrdersAsync(
            walletType: "spot",
            market: "usdttwd",
            cancellationToken);

        if (maxOrdersResult.IsFailure)
        {
            return Result<IReadOnlyCollection<Order>>.Failure(maxOrdersResult.Error);
        }

        var maxOrders = maxOrdersResult.Value;

        var orderResults = maxOrders
            .Select(mo =>
            {

                var tradeType = TradeType.FromName(mo.Side, ignoreCase: true).ThrowIfNull().Value;

                var orderStatus = mo.State switch
                {
                    "wait" => OrderStatus.Pending,
                    "convert" or "done" => OrderStatus.Executed,
                    "cancel" => OrderStatus.Cancelled,
                    _ => throw new InvalidOperationException($"Unknown max order status: {mo.State}")
                };

                return Order.Create(
                    id: OrderId.From(mo.Id).ThrowIfFailure().Value,
                    accountId: AccountId.From("000-8283782").ThrowIfFailure().Value,
                    symbol: _symbolMapper.FromExternalSymbol(mo.Market),
                    tradeType: tradeType,
                    type: OrderType.FromName(mo.OrderType, ignoreCase: true).ThrowIfNull().Value,
                    timeInForce: TimeInForce.ImmediateOrCancel,
                    quantity: mo.Volume,
                    remainingQuantity: mo.RemainingVolume,
                    executedQuantity: mo.ExecutedVolume,
                    status: orderStatus,
                    clientOrderId: mo.ClientOrderId,
                    createdTimeUtc: mo.CreatedTimeUtc);
            })
            .ToList();

        if (orderResults.Any(r => r.HasError))
        {
            var firstError = orderResults.First(e => e.HasError).Errors.First();
            return Result<IReadOnlyCollection<Order>>.Failure(firstError);
        }

        return Result<IReadOnlyCollection<Order>>.With(orderResults
            .Select(r => r.Value)
            .ToList());
    }

    private async Task<Result> OnStartedAsync(CancellationToken cancellationToken = default)
    {
        return Result.Success;
    }
}