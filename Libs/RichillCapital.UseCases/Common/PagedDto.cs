namespace RichillCapital.UseCases.Common;

public sealed record PagedDto<T>
{
    public required IEnumerable<T> Items { get; init; }

    public required int TotalCount { get; init; }
}