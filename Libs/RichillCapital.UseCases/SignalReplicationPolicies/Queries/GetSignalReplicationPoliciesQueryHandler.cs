
using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.SignalReplicationPolicies.Queries;

internal sealed class GetSignalReplicationPoliciesQueryHandler(
    IReadOnlyRepository<SignalReplicationPolicy> _signalReplicationPolicyRepository) :
    IQueryHandler<GetSignalReplicationPolicyQuery, ErrorOr<SignalReplicationPolicyDto>>
{
    public async Task<ErrorOr<SignalReplicationPolicyDto>> Handle(
        GetSignalReplicationPolicyQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = SignalReplicationPolicyId.From(query.SignalReplicationPolicyId);

        if (validationResult.IsFailure)
        {
            return ErrorOr<SignalReplicationPolicyDto>.WithError(validationResult.Error);
        }

        var id = validationResult.Value;

        var maybePolicy = await _signalReplicationPolicyRepository.GetByIdAsync(id, cancellationToken);

        if (maybePolicy.IsNull)
        {
            return ErrorOr<SignalReplicationPolicyDto>.WithError(SignalReplicationPolicyErrors.NotFound(id));
        }

        var policy = maybePolicy.Value;

        return ErrorOr<SignalReplicationPolicyDto>.With(policy.ToDto());
    }
}
