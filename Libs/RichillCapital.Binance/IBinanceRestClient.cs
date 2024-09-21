using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Binance;

public interface IBinanceRestClient
{
    Task<Result> NewOrderAsync(string symbol, string side, string type, decimal quantity, string clientOrderId, CancellationToken cancellationToken = default);
}
