namespace RichillCapital.UseCases.Signals;

public sealed record SignalDto
{
    public required string Id { get; init; }
    public required string SourceId { get; init; }
    public required string Origin { get; init; }
    public required string Symbol { get; init; }
    public required DateTimeOffset Time { get; init; }
    public required string TradeType { get; init; }
    public required decimal Quantity { get; init; }
    public required string Status { get; init; }
    public required long Latency { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}
