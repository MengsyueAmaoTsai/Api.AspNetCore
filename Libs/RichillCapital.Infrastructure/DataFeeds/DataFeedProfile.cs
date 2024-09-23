namespace RichillCapital.Infrastructure.DataFeeds;

public sealed record DataFeedProfile
{
    public required string Provider { get; init; }
    public required string Name { get; init; }
    public required bool StartOnBoot { get; init; }
    public required bool Enabled { get; init; }
    public required Dictionary<string, object> Arguments { get; init; }
}
