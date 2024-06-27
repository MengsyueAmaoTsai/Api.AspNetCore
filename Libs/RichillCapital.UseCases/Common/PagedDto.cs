namespace RichillCapital.UseCases.Common;

public sealed record PagedDto<T>
{
    public required int TotalCount { get; init; }
    public required int Page { get; init; }
    public required int PageSize { get; init; }
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;
    public required IEnumerable<T> Items { get; init; }
}