using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.SignalReplicationPolicies.Queries;

public sealed record ListSignalReplicationPoliciesQuery :
    IQuery<ErrorOr<IEnumerable<SignalReplicationPolicyDto>>>
{
}
