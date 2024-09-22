using RichillCapital.Domain;

namespace RichillCapital.UseCases.SignalReplicationPolicies;

internal static class SignalReplicationPolicyExtensions
{
    internal static SignalReplicationPolicyDto ToDto(this SignalReplicationPolicy policy) =>
        new()
        {
            Id = policy.Id.Value,
            UserId = policy.UserId.Value,
            SourceId = policy.SourceId.Value,
            CreatedTimeUtc = policy.CreatedTimeUtc,
        };
}