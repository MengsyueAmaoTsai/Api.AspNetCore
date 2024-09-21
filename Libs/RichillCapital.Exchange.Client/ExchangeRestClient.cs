using System.Net.Http.Json;

using Microsoft.Extensions.Logging;

using RichillCapital.Contracts.Orders;
using RichillCapital.Domain;
using RichillCapital.Http;
using RichillCapital.SharedKernel;
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
            "api/v1/orders",
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

        return await HandleResponse<OrderCreatedResponse>(response);
    }

    private async Task<Result<TResponse>> HandleResponse<TResponse>(HttpResponseMessage httpResponse)
    {
        if (!httpResponse.IsSuccessStatusCode)
        {
            var error = await httpResponse.ReadAsErrorAsync();
            _logger.LogWarning("{Error}", error);
            return Result<TResponse>.Failure(error);
        }

        try
        {
            var response = await httpResponse.ReadAsAsync<TResponse>();
            return Result<TResponse>.With(response!);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to deserialize response");
            return Result<TResponse>.Failure(Error.Unexpected("Max.HandleResponse", ex.Message));
        }
    }
}