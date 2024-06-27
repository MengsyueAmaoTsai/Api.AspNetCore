namespace RichillCapital.Contracts;

public sealed record Paged<TResponse>
{
    public required int TotalCount { get; init; }
    public required IEnumerable<TResponse> Items { get; init; }
}
