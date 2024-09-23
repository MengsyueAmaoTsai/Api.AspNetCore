namespace RichillCapital.UseCases.DataFeeds;

public sealed record DataFeedDto
{
    public required string Provider { get; init; }
    public required string Name { get; init; }
    public required string Status { get; init; }
    public required IReadOnlyDictionary<string, object> Arguments { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}
