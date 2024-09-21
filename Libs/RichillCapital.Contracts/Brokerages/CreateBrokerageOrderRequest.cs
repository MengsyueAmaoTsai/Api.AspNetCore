using Microsoft.AspNetCore.Mvc;

namespace RichillCapital.Contracts.Brokerages;

public sealed record CreateBrokerageOrderRequest
{
    [FromRoute(Name = nameof(ConnectionName))]
    public required string ConnectionName { get; init; }

    [FromBody]
    public required BrokerageOrderRequest Order { get; init; }
}

public sealed record BrokerageOrderRequest
{
    public required string Symbol { get; init; }
    public required string TradeType { get; init; }
    public required string OrderType { get; init; }
    public required string TimeInForce { get; init; }
    public required decimal Quantity { get; init; }
}