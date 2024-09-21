using Newtonsoft.Json;

namespace RichillCapital.Binance.Spot;

internal static class BinanceRestApiRoutes
{
    internal static class General
    {
        public const string TestConnectivity = "api/v3/ping";
        public const string ExchangeInfo = "api/v3/exchangeInfo";
        public const string CheckServerTime = "api/v3/time";
    }

    internal static class MarketData
    {
    }

    internal static class Trading
    {
    }

    internal static class Account
    {
    }

    internal static class UserDataStream
    {
    }
}

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