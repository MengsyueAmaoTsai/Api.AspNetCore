namespace RichillCapital.UseCases.SignalReplicationPolicies;

public sealed record SignalReplicationPolicyDto
{
    public required string Id { get; init; }
    public required string UserId { get; init; }
    public required string SourceId { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}