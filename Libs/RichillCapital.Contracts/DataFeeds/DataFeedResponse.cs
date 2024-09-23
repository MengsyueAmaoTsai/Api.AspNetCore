using RichillCapital.UseCases.DataFeeds;

namespace RichillCapital.Contracts.DataFeeds;

public record DataFeedResponse
{
    public required string Provider { get; init; }
    public required string Name { get; init; }
    public required string Status { get; init; }
    public required IReadOnlyDictionary<string, object> Arguments { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}

public sealed record DataFeedDetailsResponse : DataFeedResponse
{
}

public static class DataFeedResponseMapping
{
    public static DataFeedResponse ToResponse(this DataFeedDto dto) =>
        new()
        {
            Provider = dto.Provider,
            Name = dto.Name,
            Status = dto.Status,
            Arguments = dto.Arguments,
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };

    public static DataFeedDetailsResponse ToDetailsResponse(this DataFeedDto dto) =>
        new()
        {
            Provider = dto.Provider,
            Name = dto.Name,
            Status = dto.Status,
            Arguments = dto.Arguments,
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };
}