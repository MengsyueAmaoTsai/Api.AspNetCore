using RichillCapital.Domain;

namespace RichillCapital.UseCases.SignalSourceSubscriptions;

public sealed record SignalSourceSubscriptionDto
{
    public required string Id { get; init; }
}

internal static class SignalSourceSubscriptionExtensions
{
    internal static SignalSourceSubscriptionDto ToDto(
        this SignalSourceSubscription subscription) =>
        new()
        {
            Id = subscription.Id.Value,
        };
}