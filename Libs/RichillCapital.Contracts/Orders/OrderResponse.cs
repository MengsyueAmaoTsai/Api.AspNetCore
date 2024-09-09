using RichillCapital.UseCases.Orders;

namespace RichillCapital.Contracts.Orders;

public record OrderResponse
{
    public required string Id { get; init; }
    public required string TradeType { get; init; }
    public required string Symbol { get; init; }
    public required string Type { get; init; }
    public required string TimeInForce { get; init; }
    public required decimal Quantity { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}

public sealed record OrderDetailsResponse : OrderResponse
{
}

public static class OrderResponseMapping
{
    public static OrderResponse ToResponse(this OrderDto dto) =>
        new()
        {
            Id = dto.Id,
            TradeType = dto.TradeType,
            Symbol = dto.Symbol,
            Type = dto.Type,
            TimeInForce = dto.TimeInForce,
            Quantity = dto.Quantity,
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };

    public static OrderDetailsResponse ToDetailsResponse(this OrderDto dto) =>
        new()
        {
            Id = dto.Id,
            TradeType = dto.TradeType,
            Symbol = dto.Symbol,
            Type = dto.Type,
            TimeInForce = dto.TimeInForce,
            Quantity = dto.Quantity,
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };
}