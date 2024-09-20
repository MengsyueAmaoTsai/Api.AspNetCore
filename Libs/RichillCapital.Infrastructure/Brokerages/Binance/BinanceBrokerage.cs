using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel.Monads;

internal sealed class BinanceBrokerage(
    ILogger<BinanceBrokerage> _logger,
    string name) :
    Brokerage("Binance", name)
{
    public override Task<Result> StartAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override Task<Result> StopAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}