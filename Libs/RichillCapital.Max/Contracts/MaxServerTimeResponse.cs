using Newtonsoft.Json;

using RichillCapital.Serialization.Json.Converters;

namespace RichillCapital.Max.Contracts;

public sealed record MaxServerTimeResponse
{
    [JsonProperty("timestamp")]
    [JsonConverter(typeof(TimestampDateTimeOffsetConverter))]
    public required DateTimeOffset ServerTime { get; init; }
}
