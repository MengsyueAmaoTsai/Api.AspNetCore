using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Events;

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
            RemainingQuantity = order.RemainingQuantity,
            ExecutedQuantity = order.ExecutedQuantity,
            Status = order.Status.Name,
            ClientOrderId = order.ClientOrderId,
            CreatedTimeUtc = order.CreatedTimeUtc,
        };

    internal static void LogOrderDomainEvent<T>(
        this ILogger<T> logger, OrderDomainEvent domainEvent) =>
            logger.LogInformation(
            "[{EventName}] - Order {OrderId} for Account {AccountId} has been {Status} at {UpdatedTimeUtc}. " +
            "{TradeType} {Quantity} {Symbol} @ {LimitPrice} {OrderType} {TimeInForce}. " +
            "Executed: {ExecutedQuantity} / Remaining: {RemainingQuantity}. ",
            domainEvent.GetType().Name,
            domainEvent.OrderId,
            domainEvent.AccountId,
            domainEvent.Status,
            domainEvent.CreatedTimeUtc.ToString("HH:mm:ss.fff dd-MM-yyyy"),
            domainEvent.TradeType,
            domainEvent.Quantity,
            domainEvent.Symbol,
            domainEvent.OrderType,
            domainEvent.OrderType,
            domainEvent.TimeInForce,
            domainEvent.ExecutedQuantity,
            domainEvent.RemainingQuantity);
}