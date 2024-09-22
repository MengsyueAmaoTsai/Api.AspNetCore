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

    private async Task<Result> OnStartedAsync(CancellationToken cancellationToken = default)
    {
        var ordersResult = await _restClient.ListOrdersAsync(
            walletType: "spot",
            market: "usdttwd",
            cancellationToken);

        if (ordersResult.IsFailure)
        {
            return Result.Failure(ordersResult.Error);
        }

        foreach (var order in ordersResult.Value)
        {
            var tradeType = TradeType.FromName(order.Side, ignoreCase: true).ThrowIfNull().Value;
            var orderType = OrderType.FromName(order.OrderType, ignoreCase: true).ThrowIfNull().Value;

            var internalOrder = Order.Create(
                id: OrderId.From(order.Id).ThrowIfFailure().Value,
                accountId: AccountId.From("000-8283782").ThrowIfFailure().Value,
                symbol: _symbolMapper.FromExternalSymbol(order.Market),
                tradeType: tradeType,
                type: orderType,
                timeInForce: TimeInForce.ImmediateOrCancel,
                quantity: order.Volume,
                remainingQuantity: order.RemainingVolume,
                executedQuantity: order.ExecutedVolume,
                status: OrderStatus.Executed,
                createdTimeUtc: order.CreatedTimeUtc)
                .ThrowIfError()
                .Value;

            _logger.LogInformation("Internal order: {order}", internalOrder);
        }

        var tradesResult = await _restClient.ListTradesAsync(
            walletType: "spot",
            cancellationToken);

        if (tradesResult.IsFailure)
        {
            return Result.Failure(tradesResult.Error);
        }

        foreach (var trade in tradesResult.Value)
        {
            _logger.LogInformation("Trade: {trade}", trade);
        }

        return Result.Success;
    }
}