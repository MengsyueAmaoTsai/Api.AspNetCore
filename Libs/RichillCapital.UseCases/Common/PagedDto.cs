namespace RichillCapital.UseCases.Common;

public sealed record PagedDto<T>
{
    internal static readonly PagedDto<T> Empty = new()
    {
        Items = []
    };

    public required IEnumerable<T> Items { get; init; }
}