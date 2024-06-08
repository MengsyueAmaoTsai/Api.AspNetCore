namespace RichillCapital.Contracts;

public sealed record Paged<TResponse>
{
    public required IEnumerable<TResponse> Items { get; init; }
}
