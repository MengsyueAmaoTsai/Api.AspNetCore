
using System.Net.Http.Json;

using Microsoft.Extensions.Logging;

using RichillCapital.Contracts.Orders;
using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Exchange.Client;

internal sealed class ExchangeRestClient(
    ILogger<ExchangeRestClient> _logger,
    HttpClient _httpClient) :
    IExchangeRestClient
{
    public async Task<Result<OrderCreatedResponse>> CreateOrderAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "ai/v1/orders",
            new CreateOrderRequest
            {
                AccountId = "000-8283782",
                Symbol = "BINANCE:BTCUSDT.P",
                TradeType = TradeType.Buy.Name,
                OrderType = OrderType.Market.Name,
                TimeInForce = TimeInForce.ImmediateOrCancel.Name,
                Quantity = 0.01m,
            },
            cancellationToken);

        throw new NotImplementedException();
    }
}