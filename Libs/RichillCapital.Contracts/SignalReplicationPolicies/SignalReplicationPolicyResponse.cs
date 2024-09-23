using RichillCapital.UseCases.SignalReplicationPolicies;

namespace RichillCapital.Contracts.SignalReplicationPolicies;

public record SignalReplicationPolicyResponse
{
    public required string Id { get; init; }
    public required string UserId { get; init; }
    public required string SourceId { get; init; }
    public required IEnumerable<OrderReplicationMappingResponse> OrderReplicationMappings { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}

public sealed record SignalReplicationPolicyDetailsResponse :
    SignalReplicationPolicyResponse
{
}

public sealed record OrderReplicationMappingResponse
{
    public required string PolicyId { get; init; }
    public required string SourceSymbol { get; init; }
    public required string DestinationSymbol { get; init; }
    public required string DestinationAccountId { get; init; }
}

public static class SignalReplicationPolicyResponseMapping
{
    public static SignalReplicationPolicyResponse ToResponse(this SignalReplicationPolicyDto dto) =>
        new()
        {
            Id = dto.Id,
            UserId = dto.UserId,
            SourceId = dto.SourceId,
            OrderReplicationMappings = dto.OrderReplicationMappings
                .Select(x => x.ToResponse())
                .ToList(),
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };

    public static SignalReplicationPolicyDetailsResponse ToDetailsResponse(this SignalReplicationPolicyDto dto) =>
        new()
        {
            Id = dto.Id,
            UserId = dto.UserId,
            SourceId = dto.SourceId,
            OrderReplicationMappings = dto.OrderReplicationMappings
                .Select(x => x.ToResponse())
                .ToList(),
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };

    public static OrderReplicationMappingResponse ToResponse(this OrderReplicationMappingDto dto) =>
        new()
        {
            PolicyId = dto.PolicyId,
            SourceSymbol = dto.SourceSymbol,
            DestinationSymbol = dto.DestinationSymbol,
            DestinationAccountId = dto.DestinationAccountId,
        };
}