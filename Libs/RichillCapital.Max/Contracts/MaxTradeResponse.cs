using Newtonsoft.Json;

using RichillCapital.Serialization.Json.Converters;

namespace RichillCapital.Max.Contracts;

public sealed record MaxTradeResponse
{
    [JsonProperty("id")]
    public required string Id { get; init; }

    [JsonProperty("order_id")]
    public required string OrderId { get; init; }

    [JsonProperty("wallet_type")]
    public required string WalletType { get; init; }

    [JsonProperty("price")]
    public required decimal Price { get; init; }

    [JsonProperty("volume")]
    public required decimal Volume { get; init; }

    [JsonProperty("funds")]
    public required decimal Funds { get; init; }

    [JsonProperty("market")]
    public required string Market { get; init; }

    [JsonProperty("market_name")]
    public required string MarketName { get; init; }

    [JsonProperty("side")]
    public required string Side { get; init; }

    [JsonProperty("fee")]
    public required decimal Fee { get; init; }

    [JsonProperty("fee_currency")]
    public required string FeeCurrency { get; init; }

    [JsonProperty("fee_discounted")]
    public required bool FeeDiscounted { get; init; }

    [JsonProperty("self_trade_bid_fee")]
    public required decimal SelfTradeBidFee { get; init; }

    [JsonProperty("self_trade_bid_fee_currency")]
    public required string SelfTradeBidFeeCurrency { get; init; }

    [JsonProperty("self_trade_bid_fee_discounted")]
    public required bool SelfTradeBidFeeDiscounted { get; init; }

    [JsonProperty("self_trade_bid_order_id")]
    public required string SelfTradeBidOrderId { get; init; }

    [JsonProperty("liquidity")]
    public required string Liquidity { get; init; }

    [JsonProperty("created_at")]
    [JsonConverter(typeof(TimestampDateTimeOffsetConverter))]
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}