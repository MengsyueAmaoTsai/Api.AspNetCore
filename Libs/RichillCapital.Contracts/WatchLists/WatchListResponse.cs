using RichillCapital.UseCases.WatchLists;

namespace RichillCapital.Contracts.WatchLists;

public record WatchListResponse
{
    public required string Id { get; init; }
    public required string UserId { get; init; }
    public required string Name { get; init; }
}

public sealed record WatchListDetailsResponse : WatchListResponse
{
}

public static class WatchListResponseMapping
{
    public static WatchListResponse ToResponse(this WatchListDto dto) =>
        new()
        {
            Id = dto.Id,
            UserId = dto.UserId,
            Name = dto.Name,
        };

    public static WatchListDetailsResponse ToDetailsResponse(this WatchListDto dto) =>
        new()
        {
            Id = dto.Id,
            UserId = dto.UserId,
            Name = dto.Name,
        };
}