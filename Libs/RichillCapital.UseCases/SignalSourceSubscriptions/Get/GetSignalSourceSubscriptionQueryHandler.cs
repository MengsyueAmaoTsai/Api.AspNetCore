
using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.SignalSourceSubscriptions;

internal sealed class GetSignalSourceSubscriptionQueryHandler(
    IReadOnlyRepository<SignalSourceSubscription> _signalSourceSubscriptionRepository) :
    IQueryHandler<GetSignalSourceSubscriptionQuery, ErrorOr<SignalSourceSubscriptionDto>>
{
    public async Task<ErrorOr<SignalSourceSubscriptionDto>> Handle(
        GetSignalSourceSubscriptionQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = SignalSourceSubscriptionId.From(query.SignalSourceSubscriptionId);

        if (validationResult.IsFailure)
        {
            return ErrorOr<SignalSourceSubscriptionDto>.WithError(validationResult.Error);
        }

        var id = validationResult.Value;

        var maybeSubscription = await _signalSourceSubscriptionRepository
            .FirstOrDefaultAsync(
                subscription => subscription.Id == id,
                cancellationToken);

        if (maybeSubscription.IsNull)
        {
            var error = Error.NotFound(
                "SignalSourceSubscriptions.NotFound",
                $"Signal source subscription with id {id} not found.");

            return ErrorOr<SignalSourceSubscriptionDto>.WithError(error);
        }

        var subscription = maybeSubscription.Value;

        var dto = subscription.ToDto();

        return ErrorOr<SignalSourceSubscriptionDto>.With(dto);
    }
}