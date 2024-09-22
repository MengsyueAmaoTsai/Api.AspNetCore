using Newtonsoft.Json;

using RichillCapital.Serialization.Json.Converters;

namespace RichillCapital.Max.Contracts;

public sealed record MaxAccountBalanceResponse
{
    [JsonProperty("currency")]
    public required string Currency { get; init; }

    [JsonProperty("balance")]
    public required decimal Balance { get; init; }

    [JsonProperty("locked")]
    public required decimal Locked { get; init; }

    [JsonProperty("staked")]
    [JsonConverter(typeof(NullableDecimalConverter))]
    public required decimal Staked { get; init; }

    [JsonProperty("principal")]
    public required decimal Principal { get; init; }

    [JsonProperty("interest")]
    public required decimal Interest { get; init; }
}