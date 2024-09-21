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
        var serverTimeResult = await _restClient.GetServerTimeAsync(cancellationToken);
        var userInfoResult = await _restClient.GetUserInfoAsync(cancellationToken);

        if (serverTimeResult.IsFailure)
        {
            return Result.Failure(serverTimeResult.Error);
        }

        if (userInfoResult.IsFailure)
        {
            return Result.Failure(userInfoResult.Error);
        }

        IsConnected = true;

        _logger.LogInformation("Connected to Max. Server time: {ServerTime}", serverTimeResult.Value);
        _logger.LogInformation("User info: {UserInfo}", userInfoResult.Value);

        return await Task.FromResult(Result.Success);
    }

    public override Task<Result> StopAsync(CancellationToken cancellationToken = default)
    {
        IsConnected = false;

        return Task.FromResult(Result.Success);
    }

    public override async Task<Result> SubmitOrderAsync(Symbol symbol, TradeType tradeType, OrderType orderType, decimal quantity, string clientOrderId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Submitting order: {TradeType} {Symbol} {Quantity} @ {OrderType} with client order ID {ClientOrderId}",
            tradeType,
            symbol,
            quantity,
            orderType,
            clientOrderId);

        var submitResult = await _restClient.SubmitOrderAsync(cancellationToken);

        if (submitResult.IsFailure)
        {
            return Result.Failure(submitResult.Error);
        }

        return Result.Success;
    }
}