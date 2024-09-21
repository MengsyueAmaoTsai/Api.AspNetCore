using Newtonsoft.Json;

namespace RichillCapital.Binance;

internal sealed record BinanceErrorResponse
{
    [JsonProperty("code")]
    public required int Code { get; init; }

    [JsonProperty("msg")]
    public required string Message { get; init; }
}