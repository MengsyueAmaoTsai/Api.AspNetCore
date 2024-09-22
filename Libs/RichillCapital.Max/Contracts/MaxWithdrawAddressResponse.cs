using Newtonsoft.Json;

using RichillCapital.Serialization.Json.Converters;

namespace RichillCapital.Max.Contracts;

public sealed record MaxWithdrawAddressResponse
{
    [JsonProperty("uuid")]
    public required string Uuid { get; init; }

    [JsonProperty("currency")]
    public required string Currency { get; init; }

    [JsonProperty("network_protocol")]
    public required string NetworkProtocol { get; init; }

    [JsonProperty("address")]
    public required string Address { get; init; }

    [JsonProperty("extra_label")]
    public required string ExtraLabel { get; init; }

    [JsonProperty("is_internal")]
    public required bool IsInternal { get; init; }

    [JsonProperty("created_at")]
    [JsonConverter(typeof(TimestampDateTimeOffsetConverter))]
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}