namespace RichillCapital.Contracts;

public sealed record PagedResponse<TResponse>
{
    public required IEnumerable<TResponse> Items { get; init; }
}
