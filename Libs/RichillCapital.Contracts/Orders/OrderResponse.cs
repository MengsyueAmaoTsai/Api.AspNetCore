using RichillCapital.UseCases.Orders;

namespace RichillCapital.Contracts.Orders;

public record OrderResponse
{
    public required string Id { get; init; }
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
        };

    public static OrderDetailsResponse ToDetailsResponse(this OrderDto dto) =>
        new()
        {
            Id = dto.Id,
        };
}