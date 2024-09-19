namespace RichillCapital.UseCases.Executions;

public sealed record ExecutionDto
{
    public required string Id { get; init; }
    public required string AccountId { get; init; }
    public required string OrderId { get; init; }
    public required string PositionId { get; init; }
    public required string Symbol { get; init; }
    public required string TradeType { get; init; }
    public required string OrderType { get; init; }
    public required string TimeInForce { get; init; }
    public required decimal Quantity { get; init; }
    public required decimal Price { get; init; }
    public required decimal Commission { get; init; }
    public required decimal Tax { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}
