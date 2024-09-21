using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Binance;

public interface IBinanceSpotRestClient
{
    Task<Result> TestConnectivityAsync(CancellationToken cancellationToken = default);
}