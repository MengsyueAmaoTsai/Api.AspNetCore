namespace RichillCapital.Contracts.WatchLists;

public sealed record CreateWatchListRequest
{
    public required string UserId { get; init; }
    public required string Name { get; init; }
}
