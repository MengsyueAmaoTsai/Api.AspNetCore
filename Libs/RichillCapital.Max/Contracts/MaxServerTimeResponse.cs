using Newtonsoft.Json;

using RichillCapital.Serialization;

namespace RichillCapital.Max.Contracts;

public sealed record MaxServerTimeResponse
{
    [JsonProperty("timestamp")]
    [JsonConverter(typeof(TimestampDateTimeOffsetConverter))]
    public required DateTimeOffset ServerTime { get; init; }
}
