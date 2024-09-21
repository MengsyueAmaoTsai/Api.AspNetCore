using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Binance.Spot;

public interface IBinanceSpotRestClient
{
    Task<Result> TestConnectivityAsync(CancellationToken cancellationToken = default);
    Task<Result<BinanceServerTimeResponse>> CheckServerTimeAsync(CancellationToken cancellationToken = default);
    Task<Result<ExchangeInfoResponse>> GetExchangeInfoAsync(CancellationToken cancellationToken = default);

    Task<Result> NewOrderAsync(
        string symbol,
        string side,
        string type,
        string timeInForce,
        decimal quantity,
        decimal price,
        long recvWindow,
        long timestamp,
        CancellationToken cancellationToken = default);
}

