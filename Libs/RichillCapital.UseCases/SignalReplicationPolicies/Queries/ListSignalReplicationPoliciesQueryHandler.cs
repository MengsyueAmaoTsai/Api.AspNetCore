using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.SignalReplicationPolicies.Queries;

internal sealed class ListSignalReplicationPoliciesQueryHandler(
    IReadOnlyRepository<SignalReplicationPolicy> _signalReplicationPolicyRepository) :
    IQueryHandler<ListSignalReplicationPoliciesQuery, ErrorOr<IEnumerable<SignalReplicationPolicyDto>>>
{
    public async Task<ErrorOr<IEnumerable<SignalReplicationPolicyDto>>> Handle(
        ListSignalReplicationPoliciesQuery query,
        CancellationToken cancellationToken)
    {
        var policies = await _signalReplicationPolicyRepository.ListAsync(cancellationToken);

        return ErrorOr<IEnumerable<SignalReplicationPolicyDto>>.With(policies
            .Select(policy => policy.ToDto())
            .ToList());
    }
}