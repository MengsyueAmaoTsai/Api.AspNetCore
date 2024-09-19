namespace RichillCapital.UseCases.WatchLists;

public sealed record WatchListDto
{
    public required string Id { get; init; }
    public required string UserId { get; init; }
    public required string Name { get; init; }
}
