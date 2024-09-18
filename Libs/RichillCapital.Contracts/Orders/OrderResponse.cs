using RichillCapital.UseCases.Orders;

namespace RichillCapital.Contracts.Orders;

public record OrderResponse
{
    public required string Id { get; init; }
    public required string AccountId { get; init; }
    public required string Symbol { get; init; }
    public required string TradeType { get; init; }
    public required string Type { get; init; }
    public required string TimeInForce { get; init; }
    public required decimal Quantity { get; init; }
    public required decimal RemainingQuantity { get; init; }
    public required decimal ExecutedQuantity { get; init; }
    public required string Status { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}

public sealed record OrderDetailsResponse : OrderResponse
{
}

public static class OrderResponseMapping
{
    public static OrderResponse ToResponse(this OrderDto order) =>
        new()
        {
            Id = order.Id,
            AccountId = order.AccountId,
            Symbol = order.Symbol,
            TradeType = order.TradeType,
            Type = order.Type,
            TimeInForce = order.TimeInForce,
            Quantity = order.Quantity,
            RemainingQuantity = order.RemainingQuantity,
            ExecutedQuantity = order.ExecutedQuantity,
            Status = order.Status,
            CreatedTimeUtc = order.CreatedTimeUtc,
        };

    public static OrderDetailsResponse ToDetailsResponse(this OrderDto order) =>
        new()
        {
            Id = order.Id,
            AccountId = order.AccountId,
            Symbol = order.Symbol,
            TradeType = order.TradeType,
            Type = order.Type,
            TimeInForce = order.TimeInForce,
            Quantity = order.Quantity,
            RemainingQuantity = order.RemainingQuantity,
            ExecutedQuantity = order.ExecutedQuantity,
            Status = order.Status,
            CreatedTimeUtc = order.CreatedTimeUtc,
        };
}