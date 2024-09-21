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

        if (testResult.IsFailure)
        {
            return Result.Failure(testResult.Error);
        }

        IsConnected = true;

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
        return Result.Success;
    }
}
