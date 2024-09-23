using RichillCapital.Domain;

namespace RichillCapital.UseCases.SignalSubscriptions;

internal static class SignalSubscriptionExtensions
{
    internal static SignalSubscriptionDto ToDto(this SignalSubscription subscription) =>
        new()
        {
            Id = subscription.Id.Value,
            UserId = subscription.UserId.Value,
            SourceId = subscription.SourceId.Value,
            CreatedTimeUtc = subscription.CreatedTimeUtc,
        };
}