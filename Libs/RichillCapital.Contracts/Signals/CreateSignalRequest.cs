namespace RichillCapital.Contracts.Signals;

public sealed record CreateSignalRequest
{
    public required DateTimeOffset Time { get; init; }
    public required string Behavior { get; init; }
    public required string Side { get; init; }
    public required string Symbol { get; init; }
    public required decimal Quantity { get; init; }
    public required decimal Price { get; init; }
    public required string OrderType { get; init; }
}

public sealed record CreateSignalResponse
{
    public required string Id { get; init; }
}