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
            _logger.LogWarning("Binance server is unavailable");
            return Result.Failure(Error.Unavailable("Binance server is unavailable"));
        }

        _logger.LogInformation("Binance server is available");
        return Result.Success;
    }
}
