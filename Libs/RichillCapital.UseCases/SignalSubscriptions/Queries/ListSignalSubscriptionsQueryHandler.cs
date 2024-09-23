using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.SignalSubscriptions.Queries;

internal sealed class ListSignalSubscriptionsQueryHandler(
    IReadOnlyRepository<SignalSubscription> _signalSubscriptionRepository) :
    IQueryHandler<ListSignalSubscriptionsQuery, ErrorOr<IEnumerable<SignalSubscriptionDto>>>
{
    public async Task<ErrorOr<IEnumerable<SignalSubscriptionDto>>> Handle(
        ListSignalSubscriptionsQuery query,
        CancellationToken cancellationToken)
    {
        var subscriptions = await _signalSubscriptionRepository.ListAsync(cancellationToken);

        return ErrorOr<IEnumerable<SignalSubscriptionDto>>.With(subscriptions
            .Select(s => s.ToDto())
            .ToList());
    }
}