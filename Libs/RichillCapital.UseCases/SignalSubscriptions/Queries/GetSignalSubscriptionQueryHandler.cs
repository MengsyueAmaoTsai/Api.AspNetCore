using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.SignalSubscriptions.Queries;

internal sealed class GetSignalSubscriptionQueryHandler(
    IReadOnlyRepository<SignalSubscription> _signalSubscriptionRepository) :
    IQueryHandler<GetSignalSubscriptionQuery, ErrorOr<SignalSubscriptionDto>>
{
    public async Task<ErrorOr<SignalSubscriptionDto>> Handle(
        GetSignalSubscriptionQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = SignalSubscriptionId.From(query.SignalSubscriptionId);

        if (validationResult.IsFailure)
        {
            return ErrorOr<SignalSubscriptionDto>.WithError(validationResult.Error);
        }

        var id = validationResult.Value;

        var maybeSubscription = await _signalSubscriptionRepository.GetByIdAsync(id, cancellationToken);

        if (maybeSubscription.IsNull)
        {
            return ErrorOr<SignalSubscriptionDto>.WithError(SignalSubscriptionErrors.NotFound(id));
        }

        return ErrorOr<SignalSubscriptionDto>.With(maybeSubscription.Value.ToDto());
    }
}