namespace RichillCapital.UseCases.Trades;

public sealed record TradeDto
{
    public required string Id { get; init; }
    public required string Symbol { get; init; }
    public required string Side { get; init; }
}
