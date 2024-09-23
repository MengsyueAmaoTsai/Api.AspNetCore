using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.SignalSubscriptions.Queries;

public sealed record ListSignalSubscriptionsQuery :
    IQuery<ErrorOr<IEnumerable<SignalSubscriptionDto>>>
{
}
