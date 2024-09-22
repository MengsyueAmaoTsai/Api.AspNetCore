using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.SignalReplicationPolicies.Commands;

internal sealed class CreateSignalReplicationPolicyCommandHandler(
    IReadOnlyRepository<User> _userRepository,
    IReadOnlyRepository<SignalSource> _signalSourceRepository,
    IRepository<SignalReplicationPolicy> _signalReplicationPolicyRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<CreateSignalReplicationPolicyCommand, ErrorOr<SignalReplicationPolicyId>>
{
    public async Task<ErrorOr<SignalReplicationPolicyId>> Handle(
        CreateSignalReplicationPolicyCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = Result<(UserId, SignalSourceId)>.Combine(
            UserId.From(command.UserId),
            SignalSourceId.From(command.SourceId));

        if (validationResult.IsFailure)
        {
            return ErrorOr<SignalReplicationPolicyId>.WithError(validationResult.Error);
        }

        var (userId, sourceId) = validationResult.Value;

        if (!await _userRepository.AnyAsync(u => u.Id == userId, cancellationToken))
        {
            return ErrorOr<SignalReplicationPolicyId>.WithError(UserErrors.NotFound(userId));
        }

        if (!await _signalSourceRepository.AnyAsync(s => s.Id == sourceId, cancellationToken))
        {
            return ErrorOr<SignalReplicationPolicyId>.WithError(SignalSourceErrors.NotFound(sourceId));
        }

        var errorOrPolicy = SignalReplicationPolicy.Create(
            SignalReplicationPolicyId.NewSignalReplicationPolicyId(),
            userId,
            sourceId,
            DateTimeOffset.UtcNow);

        if (errorOrPolicy.HasError)
        {
            return ErrorOr<SignalReplicationPolicyId>.WithError(errorOrPolicy.Errors);
        }

        var policy = errorOrPolicy.Value;

        _signalReplicationPolicyRepository.Add(policy);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ErrorOr<SignalReplicationPolicyId>.With(policy.Id);
    }
}