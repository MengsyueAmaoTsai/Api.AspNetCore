using RichillCapital.Contracts.Orders;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Exchange.Client;

public interface IExchangeRestClient
{
    Task<Result<OrderCreatedResponse>> CreateOrderAsync(CancellationToken cancellationToken = default);
}
