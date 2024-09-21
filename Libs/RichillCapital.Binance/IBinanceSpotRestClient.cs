using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Binance;

public interface IBinanceSpotRestClient
{
    Task<Result> TestConnectivityAsync(CancellationToken cancellationToken = default);
    Task<Result<DateTimeOffset>> CheckServerTimeAsync(CancellationToken cancellationToken = default);
    Task<Result<ExchangeInfoResponse>> GetExchangeInfoAsync(CancellationToken cancellationToken = default);

    Task<Result> NewOrderAsync(
        string symbol = "LTCBTC",
        string side = "BUY",
        string type = "LIMIT",
        string timeInForce = "GTC",
        decimal quantity = 1,
        decimal price = 0.1m,
        long recvWindow = 5000,
        long timestamp = 1499827319559,
        CancellationToken cancellationToken = default);
}

