namespace RichillCapital.UseCases.Positions;

public sealed record PositionDto
{
    public required string Id { get; init; }
    public required string AccountId { get; init; }
    public required string Symbol { get; init; }
    public required string Side { get; init; }
    public required decimal Quantity { get; init; }
    public required decimal AveragePrice { get; init; }
    public required decimal Commission { get; init; }
    public required decimal Tax { get; init; }
    public required decimal Swap { get; init; }
    public required string Status { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}
