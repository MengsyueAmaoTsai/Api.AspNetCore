using RichillCapital.Domain;

namespace RichillCapital.UseCases.Orders;

internal static class OrderExtensions
{
    internal static OrderDto ToDto(this Order order) =>
        new()
        {
            Id = order.Id.Value,
            AccountId = order.AccountId.Value,
            Symbol = order.Symbol.Value,
            TradeType = order.TradeType.Name,
            Type = order.Type.Name,
            TimeInForce = order.TimeInForce.Name,
            Quantity = order.Quantity,
            Status = order.Status.Name,
            CreatedTimeUtc = order.CreatedTimeUtc,
        };
}