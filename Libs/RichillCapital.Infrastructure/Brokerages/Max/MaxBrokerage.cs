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
    public override async Task<Result> StartAsync(CancellationToken cancellationToken = default)
    {
        var result = await _restClient.ListOpenOrdersAsync(
            walletType: "spot",
            cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        Status = ConnectionStatus.Active;

        _logger.LogInformation("{value}", result);

        return await Task.FromResult(Result.Success);
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
            cancellationToken);

        if (submitResult.IsFailure)
        {
            return Result.Failure(submitResult.Error);
        }

        return Result.Success;
    }
}