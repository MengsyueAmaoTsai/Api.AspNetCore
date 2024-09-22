using RichillCapital.UseCases.SignalReplicationPolicies;

namespace RichillCapital.Contracts.SignalReplicationPolicies;

public record SignalReplicationPolicyResponse
{
    public required string Id { get; init; }
    public required string UserId { get; init; }
    public required string SourceId { get; init; }
    public required string TradingSymbol { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}

public sealed record SignalReplicationPolicyDetailsResponse :
    SignalReplicationPolicyResponse
{
}

public static class SignalReplicationPolicyResponseMapping
{
    public static SignalReplicationPolicyResponse ToResponse(this SignalReplicationPolicyDto dto) =>
        new()
        {
            Id = dto.Id,
            UserId = dto.UserId,
            SourceId = dto.SourceId,
            TradingSymbol = dto.TradingSymbol,
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };

    public static SignalReplicationPolicyDetailsResponse ToDetailsResponse(this SignalReplicationPolicyDto dto) =>
        new()
        {
            Id = dto.Id,
            UserId = dto.UserId,
            SourceId = dto.SourceId,
            TradingSymbol = dto.TradingSymbol,
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };
}