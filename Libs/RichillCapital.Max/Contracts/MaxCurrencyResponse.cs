using Newtonsoft.Json;

namespace RichillCapital.Max.Contracts;

public sealed record MaxCurrencyResponse
{
    [JsonProperty("currency")]
    public required string Currency { get; init; }

    [JsonProperty("type")]
    public required string Type { get; init; }

    [JsonProperty("precision")]
    public required int Precision { get; init; }

    [JsonProperty("m_wallet_supported")]
    public required bool MWalletSupported { get; init; }

    [JsonProperty("m_wallet_mortgageable")]
    public required bool MWalletMortgageable { get; init; }

    [JsonProperty("m_wallet_borrowable")]
    public required bool MWalletBorrowable { get; init; }

    // [JsonProperty("networks")]
    // [JsonProperty("staking")]
}

