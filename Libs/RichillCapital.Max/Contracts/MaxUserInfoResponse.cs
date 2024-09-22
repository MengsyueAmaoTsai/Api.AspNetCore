using Newtonsoft.Json;

namespace RichillCapital.Max.Contracts;

public sealed record MaxUserInfoResponse
{
    [JsonProperty("email")]
    public required string Email { get; init; }

    [JsonProperty("level")]
    public required int Level { get; init; }

    [JsonProperty("m_wallet_enabled")]
    public required bool MWalletEnabled { get; init; }

    [JsonProperty("current_vip_level")]
    public required MaxUserInfoVipLevelResponse VipLevel { get; init; }

    [JsonProperty("next_vip_level")]
    public required MaxUserInfoVipLevelResponse NextVipLevel { get; init; }
}

public sealed record MaxUserInfoVipLevelResponse
{
    [JsonProperty("level")]
    public required int Level { get; init; }

    [JsonProperty("minimum_trading_volume")]
    public required decimal MinimumTradingVolume { get; init; }

    [JsonProperty("minimum_staking_volume")]
    public required decimal MinimumStakingVolume { get; init; }

    [JsonProperty("maker_fee")]
    public required decimal MakerFee { get; init; }

    [JsonProperty("taker_fee")]
    public required decimal TakerFee { get; init; }
}