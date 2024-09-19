using RichillCapital.Domain;

namespace RichillCapital.UseCases.WatchLists;

internal static class WatchListExtensions
{
    internal static WatchListDto ToDto(this WatchList watchList) =>
        new WatchListDto
        {
            Id = watchList.Id.Value,
            UserId = watchList.UserId.Value,
            Name = watchList.Name,
        };
}