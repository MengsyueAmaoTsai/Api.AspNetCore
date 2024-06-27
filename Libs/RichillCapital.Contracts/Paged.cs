namespace RichillCapital.Contracts;

public sealed record Paged<TResponse>
{
    public required int TotalCount { get; init; }
    public required int Page { get; init; }
    public required int PageSize { get; init; }
    public bool HasNext => Page * PageSize < TotalCount;
    public bool HasPrevious => Page > 1;
    public required IEnumerable<TResponse> Items { get; init; }
}
