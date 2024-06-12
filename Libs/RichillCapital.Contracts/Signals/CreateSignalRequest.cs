namespace RichillCapital.Contracts.Signals;

public sealed record CreateSignalRequest
{
    public required DateTimeOffset Time { get; init; }
    public required string PositionBehavior { get; init; }
    public required string TradeType { get; init; }
    public required string Symbol { get; init; }
    public required decimal Quantity { get; init; }
}

public sealed record CreateSignalResponse
{
    public required string Id { get; init; }
}