using Newtonsoft.Json;

using RichillCapital.Serialization;

namespace RichillCapital.Binance;

public sealed record BinanceServerTimeResponse
{
    [JsonProperty("serverTime")]
    [JsonConverter(typeof(TimestampDateTimeOffsetConverter))]
    public required DateTimeOffset ServerTime { get; init; }
}
