namespace RichillCapital.Contracts.Orders;

public sealed record OrderResponse
{
    public required string Id { get; init; }
    public required string AccountId { get; init; }
    public required string Symbol { get; init; }
    public required string TradeType { get; init; }
    public required decimal Quantity { get; init; }
    public required string OrderType { get; init; }
    public required string TimeInForce { get; init; }
    public required string Status { get; init; }
}