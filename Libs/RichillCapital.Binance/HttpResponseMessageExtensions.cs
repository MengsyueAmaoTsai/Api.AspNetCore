using Newtonsoft.Json;

using RichillCapital.Http;
using RichillCapital.SharedKernel;

namespace RichillCapital.Binance;

internal static class HttpResponseMessageExtensions
{
    internal static async Task<Error> ReadAsErrorAsync(
        this HttpResponseMessage httpResponse,
        CancellationToken cancellationToken = default)
    {
        var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

        return BinanceErrors.Create(
            httpResponse.GetErrorType(),
            JsonConvert.DeserializeObject<BinanceErrorResponse>(content)!);
    }
}