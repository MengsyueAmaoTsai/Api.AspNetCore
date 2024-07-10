namespace RichillCapital.Contracts.Orders;

public sealed record CreateOrderResponse
{
    public required string OrderId { get; init; }
}