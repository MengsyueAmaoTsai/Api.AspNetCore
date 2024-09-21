using Newtonsoft.Json;

namespace RichillCapital.Binance.Spot;

internal static class HttpResponseMessageExtensions
{
    internal static async Task<BinanceErrorResponse> ReadAsErrorResponseAsync(
        this HttpResponseMessage response,
        CancellationToken cancellationToken = default)
    {
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonConvert.DeserializeObject<BinanceErrorResponse>(content)!;
    }
}