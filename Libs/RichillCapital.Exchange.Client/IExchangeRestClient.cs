using RichillCapital.Contracts.Orders;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Exchange.Client;

public interface IExchangeRestClient
{
    Task<Result<OrderCreatedResponse>> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken = default);
}
