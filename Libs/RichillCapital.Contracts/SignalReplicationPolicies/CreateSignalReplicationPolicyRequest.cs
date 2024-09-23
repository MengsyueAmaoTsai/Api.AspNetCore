namespace RichillCapital.Contracts.SignalReplicationPolicies;

public sealed record CreateSignalReplicationPolicyRequest
{
    public required string UserId { get; init; }
    public required string SourceId { get; init; }
    public required decimal Multiplier { get; init; }
}
