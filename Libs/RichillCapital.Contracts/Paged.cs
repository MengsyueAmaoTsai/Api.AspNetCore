namespace RichillCapital.Contracts;

public sealed record Paged<TResponse>
{
    public required IEnumerable<TResponse> Items { get; init; }

    public required int TotalCount { get; init; }
}
