using RichillCapital.UseCases.SignalSubscriptions;

namespace RichillCapital.Contracts.SignalSubscriptions;

public record SignalSubscriptionResponse
{
    public required string Id { get; init; }
    public required string UserId { get; init; }
    public required string SourceId { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}

public sealed record SignalSubscriptionDetailsResponse : SignalSubscriptionResponse
{
}

public static class SignalSubscriptionResponseMapping
{
    public static SignalSubscriptionResponse ToResponse(this SignalSubscriptionDto dto) =>
        new()
        {
            Id = dto.Id,
            UserId = dto.UserId,
            SourceId = dto.SourceId,
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };

    public static SignalSubscriptionDetailsResponse ToDetailsResponse(this SignalSubscriptionDto dto) =>
        new()
        {
            Id = dto.Id,
            UserId = dto.UserId,
            SourceId = dto.SourceId,
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };
}