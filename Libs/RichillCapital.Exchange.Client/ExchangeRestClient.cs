using System.Net.Http.Json;

using Microsoft.Extensions.Logging;

using RichillCapital.Contracts.Orders;
using RichillCapital.Http.Client;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Exchange.Client;

internal sealed class ExchangeRestClient(
    ILogger<ExchangeRestClient> _logger,
    HttpClient _httpClient) :
    IExchangeRestClient
{
    public async Task<Result<OrderCreatedResponse>> CreateOrderAsync(
        CreateOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "api/v1/orders",
            request,
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