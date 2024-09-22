using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Errors;

public static class SignalReplicationPolicyErrors
{
    public static Error NotFound(SignalReplicationPolicyId id) =>
        Error.NotFound("SignalReplicationPolicy.NotFound", $"Signal replication policy with id {id.Value} not found.");
}