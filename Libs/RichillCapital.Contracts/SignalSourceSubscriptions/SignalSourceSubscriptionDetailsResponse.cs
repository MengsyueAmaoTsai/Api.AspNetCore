using RichillCapital.UseCases.SignalSourceSubscriptions;

namespace RichillCapital.Contracts.SignalSourceSubscriptions;

public sealed record SignalSourceSubscriptionDetailsResponse
{
    public required string Id { get; init; }
}

public static class SignalSourceSubscriptionDetailsResponseMapping
{
    public static SignalSourceSubscriptionDetailsResponse ToDetailsResponse(
        this SignalSourceSubscriptionDto dto) =>
        new()
        {
            Id = dto.Id
        };
}