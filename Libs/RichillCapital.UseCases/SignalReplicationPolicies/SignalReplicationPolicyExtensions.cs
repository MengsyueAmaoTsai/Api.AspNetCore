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
            OrderReplicationMappings = policy.ReplicationMappings
                .Select(x => x.ToDto())
                .ToList(),
            CreatedTimeUtc = policy.CreatedTimeUtc,
        };

    internal static OrderReplicationMappingDto ToDto(this OrderReplicationMapping mapping) =>
        new()
        {
            PolicyId = mapping.SignalReplicationPolicyId.Value,
            SourceSymbol = mapping.SourceSymbol.Value,
            DestinationSymbol = mapping.DestinationSymbol.Value,
            DestinationAccountId = mapping.DestinationAccountId.Value,
        };
}