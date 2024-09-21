using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Binance;

public interface IBinanceRestClient
{
    Task<Result<int>> PingAsync(CancellationToken cancellationToken = default);
    Task<Result<BinanceServerTimeResponse>> GetServerTimeAsync(CancellationToken cancellationToken = default);
    Task<Result> NewOrderAsync(string symbol, string side, string type, decimal quantity, string clientOrderId, CancellationToken cancellationToken = default);
}
