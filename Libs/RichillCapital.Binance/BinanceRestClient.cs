using Microsoft.Extensions.Logging;

using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Binance;

public interface IBinanceRestClient
{
    Task<Result> NewOrderAsync(string symbol, string side, string type, decimal quantity, CancellationToken cancellationToken = default);
}

internal sealed class BinanceRestClient(
    ILogger<BinanceRestClient> _logger,
    HttpClient _httpClient) :
    IBinanceRestClient
{
    public async Task<Result> NewOrderAsync(
        string symbol,
        string side,
        string type,
        decimal quantity,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "fapi/v1/order")
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "symbol", symbol },
                { "side", side },
                { "type", type },
                { "quantity", quantity.ToString() }
            })
        };

        var response = await _httpClient.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Failure(Error.Unexpected("Failed to send request to Binance."));
        }

        return Result.Success;
    }
}
