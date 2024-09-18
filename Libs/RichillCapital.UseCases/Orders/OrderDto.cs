namespace RichillCapital.UseCases.Orders;

public sealed record OrderDto
{
    public required string Id { get; init; }
    public required string AccountId { get; init; }
    public required string Symbol { get; init; }
    public required string TradeType { get; init; }
    public required string Type { get; init; }
    public required string TimeInForce { get; init; }
    public required decimal Quantity { get; init; }
    public required string Status { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}
