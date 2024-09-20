using Microsoft.AspNetCore.Mvc;

namespace RichillCapital.Contracts.WatchLists;

public sealed record UpdateWatchListRequest
{
    [FromRoute(Name = "watchListId")]
    public required string WatchListId { get; init; }

    [FromBody]
    public required UpdateWatchListRequestBody Body { get; init; }
}

public sealed record UpdateWatchListRequestBody
{
    public required string Name { get; init; }
}