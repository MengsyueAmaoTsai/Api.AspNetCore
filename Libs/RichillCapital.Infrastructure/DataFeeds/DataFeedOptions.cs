namespace RichillCapital.Infrastructure.DataFeeds;

public sealed record DataFeedOptions
{
    internal const string SectionKey = "DataFeeds";

    public required IEnumerable<DataFeedProfile> Profiles { get; init; }
}
