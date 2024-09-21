using Newtonsoft.Json;

namespace RichillCapital.Binance;

public sealed record BinanceErrorResponse
{
    [JsonProperty("code")]
    public required int Code { get; init; }

    [JsonProperty("msg")]
    public required string Message { get; init; }
}

public sealed record BinanceServerTimeResponse
{
    [JsonProperty("serverTime")]
    public required long ServerTime { get; init; }
}

public sealed record ExchangeInfoResponse
{
    [JsonProperty("timezone")]
    public required string Timezone { get; init; }

    [JsonProperty("serverTime")]
    public required long ServerTime { get; init; }
    // [JsonProperty("rateLimits")]
    // [JsonProperty("exchangeFilters")]
    // [JsonProperty("symbols")]
    // [JsonProperty("sors")]
}