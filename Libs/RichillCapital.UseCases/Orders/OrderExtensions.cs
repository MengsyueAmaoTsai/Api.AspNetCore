using RichillCapital.Domain;

namespace RichillCapital.UseCases.Orders;

internal static class OrderExtensions
{
    public static OrderDto ToDto(this Order order)
    {
        return new OrderDto
        {
            Id = order.Id.Value,
            TradeType = order.TradeType.Name,
            Symbol = order.Symbol.Value,
            OrderType = order.Type.Name,
            TimeInForce = order.TimeInForce.Name,
            Quantity = order.Quantity,
            Status = order.Status.Name,
            CreatedTimeUtc = order.CreatedTimeUtc,
        };
    }
}