namespace RichillCapital.UseCases.Trades;

public sealed record TradeDto
{
    public required string Id { get; init; }
    public required string AccountId { get; init; }
    public required string Symbol { get; init; }
    public required string Side { get; init; }
    public required decimal Quantity { get; init; }
    public required decimal EntryPrice { get; init; }
    public required DateTimeOffset EntryTimeUtc { get; init; }
    public required decimal ExitPrice { get; init; }
    public required DateTimeOffset ExitTimeUtc { get; init; }
    public required decimal Commission { get; init; }
    public required decimal Tax { get; init; }
    public required decimal Swap { get; init; }
    public required decimal ProfitLoss { get; init; }
}
