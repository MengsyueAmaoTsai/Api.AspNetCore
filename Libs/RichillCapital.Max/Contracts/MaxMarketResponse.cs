using Newtonsoft.Json;

namespace RichillCapital.Max.Contracts;

public sealed record MaxMarketResponse
{
    [JsonProperty("id")]
    public required string Id { get; init; }

    [JsonProperty("status")]
    public required string Status { get; init; }

    [JsonProperty("base_unit")]
    public required string BaseUnit { get; init; }

    [JsonProperty("quote_unit")]
    public required string QuoteUnit { get; init; }

    [JsonProperty("base_unit_precision")]
    public required int BaseUnitPrecision { get; init; }

    [JsonProperty("quote_unit_precision")]
    public required int QuoteUnitPrecision { get; init; }

    [JsonProperty("min_base_amount")]
    public required decimal MinBaseAmount { get; init; }

    [JsonProperty("min_quote_amount")]
    public required decimal MinQuoteAmount { get; init; }

    [JsonProperty("m_wallet_supported")]
    public required bool MWalletSupported { get; init; }
}