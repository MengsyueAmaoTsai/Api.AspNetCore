using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.SignalReplicationPolicies.Queries;

public sealed record GetSignalReplicationPolicyQuery :
    IQuery<ErrorOr<SignalReplicationPolicyDto>>
{
    public required string SignalReplicationPolicyId { get; init; }
}
