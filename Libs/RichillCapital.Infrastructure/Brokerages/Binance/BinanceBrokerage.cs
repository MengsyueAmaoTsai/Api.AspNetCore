using Microsoft.Extensions.Logging;

using RichillCapital.Binance;
using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel.Monads;

internal sealed class BinanceBrokerage(
    ILogger<BinanceBrokerage> _logger,
    BinanceRestService _restService,
    string name) :
    Brokerage("Binance", name)
{
    public override async Task<Result> StartAsync(CancellationToken cancellationToken = default)
    {
        if (IsConnected)
        {
            return Result.Failure(BrokerageErrors.AlreadyStarted(Name));
        }

        await _restService.TestConnectivityAsync(cancellationToken);

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
        var result = await _restService.SendOrderAsync(
            symbol: string.Empty,
            side: string.Empty,
            type: string.Empty,
            cancellationToken);

        return result;
    }
}
