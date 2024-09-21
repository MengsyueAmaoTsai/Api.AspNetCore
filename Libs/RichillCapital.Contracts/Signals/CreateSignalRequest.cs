namespace RichillCapital.Contracts.Signals;

public sealed record CreateSignalRequest
{
    public required DateTimeOffset Time { get; init; }
    public required string SourceId { get; init; }
    public required string Origin { get; init; }
    public required string Symbol { get; init; }
    public required string TradeType { get; init; }
    public required string OrderType { get; init; }
    public required decimal Quantity { get; init; }
}
