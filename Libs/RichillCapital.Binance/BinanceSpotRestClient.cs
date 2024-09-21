using Microsoft.Extensions.Logging;

using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Binance;

internal sealed class BinanceSpotRestClient(
    ILogger<BinanceSpotRestClient> _logger,
    HttpClient _httpClient) :
    IBinanceSpotRestClient
{
    public async Task<Result> TestConnectivityAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync("api/v3/ping", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

            _logger.LogError(
                "Failed to test connectivity: {statusCode} {errorContent}",
                response.StatusCode,
                errorContent);

            return Result.Failure(Error.Unexpected("Failed to test connectivity"));
        }

        return Result.Success;
    }
}
