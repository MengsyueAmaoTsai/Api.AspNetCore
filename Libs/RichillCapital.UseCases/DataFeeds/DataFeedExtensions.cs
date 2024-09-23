using RichillCapital.Domain.DataFeeds;

namespace RichillCapital.UseCases.DataFeeds;

internal static class DataFeedExtensions
{
    internal static DataFeedDto ToDto(this IDataFeed dataFeed) =>
        new()
        {
            Provider = dataFeed.Provider,
            Name = dataFeed.Name,
            Status = dataFeed.Status.Name,
            Arguments = dataFeed.Arguments,
            CreatedTimeUtc = dataFeed.CreatedTimeUtc,
        };
}