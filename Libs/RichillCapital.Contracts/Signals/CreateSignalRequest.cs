namespace RichillCapital.Contracts.Signals;

public sealed record CreateSignalRequest
{
    public DateTimeOffset Time { get; init; }
    public string? SourceId { get; init; } = string.Empty;
    public string? Origin { get; init; } = string.Empty;
    public string? Symbol { get; init; } = string.Empty;
    public string? TradeType { get; init; } = string.Empty;
    public string? OrderType { get; init; } = string.Empty;
    public decimal Quantity { get; init; } = 0;
}
