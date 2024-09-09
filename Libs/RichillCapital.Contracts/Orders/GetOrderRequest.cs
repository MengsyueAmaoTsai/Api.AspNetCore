using Microsoft.AspNetCore.Mvc;

namespace RichillCapital.Contracts.Orders;

public sealed record GetOrderRequest
{
    [FromRoute(Name = "orderId")]
    public required string OrderId { get; init; }
}
