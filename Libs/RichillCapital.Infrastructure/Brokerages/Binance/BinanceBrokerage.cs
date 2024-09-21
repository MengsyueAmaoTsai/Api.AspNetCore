using Microsoft.Extensions.Logging;

using RichillCapital.Binance;
using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel.Monads;

internal sealed class BinanceBrokerage(
    ILogger<BinanceBrokerage> _logger,
    IBinanceSpotRestClient _spotRestClient,
    string name) :
    Brokerage("Binance", name)
{
    public override async Task<Result> StartAsync(CancellationToken cancellationToken = default)
    {
        if (IsConnected)
        {
            return Result.Failure(BrokerageErrors.AlreadyStarted(Name));
        }

        var testResult = await _spotRestClient.TestConnectivityAsync(cancellationToken);
        var serverTimeResult = await _spotRestClient.CheckServerTimeAsync(cancellationToken);
        var exchangeInfoResult = await _spotRestClient.GetExchangeInfoAsync(cancellationToken);
        if (testResult.IsFailure)
        {
            return Result.Failure(testResult.Error);
        }

        if (serverTimeResult.IsFailure)
        {
            return Result.Failure(serverTimeResult.Error);
        }

        IsConnected = true;

        _logger.LogInformation(
            "Connected to Binance. Server time: {serverTime}",
            serverTimeResult.Value);

        return Result.Success;
    }

    public override Task<Result> StopAsync(CancellationToken cancellationToken = default)
    {
        if (!IsConnected)
        {
            return Task.FromResult(Result.Failure(BrokerageErrors.AlreadyStopped(Name)));
        }

        IsConnected = false;

        return Task.FromResult(Result.Success);
    }

    public override async Task<Result> SubmitOrderAsync(CancellationToken cancellationToken = default)
    {
        return await _spotRestClient.NewOrderAsync(
            symbol: "LTCBTC",
            side: "BUY",
            type: "LIMIT",
            timeInForce: "GTC",
            quantity: 1,
            price: 0.1m,
            recvWindow: 5000,
            timestamp: DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            cancellationToken: cancellationToken);
    }
}
