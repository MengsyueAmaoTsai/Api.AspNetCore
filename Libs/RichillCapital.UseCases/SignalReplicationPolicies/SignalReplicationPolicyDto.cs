namespace RichillCapital.UseCases.SignalReplicationPolicies;

public sealed record SignalReplicationPolicyDto
{
    public required string Id { get; init; }
    public required string UserId { get; init; }
    public required string SourceId { get; init; }
    public required IEnumerable<OrderReplicationMappingDto> OrderReplicationMappings { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}

public sealed record OrderReplicationMappingDto
{
    public required string PolicyId { get; init; }
    public required string SourceSymbol { get; init; }
    public required string DestinationSymbol { get; init; }
    public required string DestinationAccountId { get; init; }
}