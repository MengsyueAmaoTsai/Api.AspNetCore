using Newtonsoft.Json;

using RichillCapital.Serialization;
using RichillCapital.Serialization.JsonConverters;

namespace RichillCapital.Max.Contracts;

public sealed record MaxOrderResponse
{
    [JsonProperty("id")]
    public required string Id { get; init; }

    [JsonProperty("wallet_type")]
    public required string WalletType { get; init; }

    [JsonProperty("market")]
    public required string Market { get; init; }

    [JsonProperty("client_oid")]
    public required string ClientOrderId { get; init; }

    [JsonProperty("group_id")]
    public required string GroupId { get; init; }

    [JsonProperty("side")]
    public required string Side { get; init; }

    [JsonProperty("state")]
    public required string State { get; init; }

    [JsonProperty("ord_type")]
    public required string OrderTye { get; init; }

    [JsonProperty("price")]
    [JsonConverter(typeof(NullableDecimalConverter))]
    public required decimal Price { get; init; }

    [JsonProperty("stop_price")]
    [JsonConverter(typeof(NullableDecimalConverter))]
    public required decimal StopPrice { get; init; }

    [JsonProperty("avg_price")]
    public required decimal AveragePrice { get; init; }

    [JsonProperty("volume")]
    public required decimal Volume { get; init; }

    [JsonProperty("remaining_volume")]
    public required decimal RemainingVolume { get; init; }

    [JsonProperty("executed_volume")]
    public required decimal ExecutedVolume { get; init; }

    [JsonProperty("trades_count")]
    public required int TradesCount { get; init; }

    [JsonProperty("created_at")]
    [JsonConverter(typeof(TimestampDateTimeOffsetConverter))]
    public required DateTimeOffset CreatedTimeUtc { get; init; }

    [JsonProperty("updated_at")]
    [JsonConverter(typeof(TimestampDateTimeOffsetConverter))]
    public required DateTimeOffset UpdatedTimeUtc { get; init; }
}