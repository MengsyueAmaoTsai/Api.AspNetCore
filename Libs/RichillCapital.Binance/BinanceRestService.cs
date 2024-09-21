using System.Net.Http.Json;

using Microsoft.Extensions.Logging;

using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Binance;

public sealed class BinanceRestService(
    ILogger<BinanceRestService> _logger,
    HttpClient _httpClient)
{
    public async Task<Result> TestConnectivityAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync("api/v3/ping", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return await HandleErrorResponseAsync(response, cancellationToken);
        }

        _logger.LogInformation("Binance server is available");
        return Result.Success;
    }

    public async Task<Result> SendOrderAsync(
        string symbol,
        string side,
        string type,
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync("fapi/v1/order", new
        {
            Symbol = symbol,
            Side = side,
            Type = type,
        });

        if (!response.IsSuccessStatusCode)
        {
            return await HandleErrorResponseAsync(response, cancellationToken);
        }

        _logger.LogInformation("Order sent to Binance server");
        return Result.Success;
    }

    private async Task<Result> HandleErrorResponseAsync(
        HttpResponseMessage response,
        CancellationToken cancellationToken = default)
    {
        var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

        _logger.LogError("Binance server returned an error: {ErrorContent}", errorContent);

        return Result.Failure(Error.Unavailable("Binance server returned an error"));
    }
}
