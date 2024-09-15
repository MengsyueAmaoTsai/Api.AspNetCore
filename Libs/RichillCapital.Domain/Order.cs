using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Order : Entity<OrderId>
{
    private Order(
        OrderId id,
        Symbol symbol,
        TradeType tradeType,
        OrderType type,
        TimeInForce timeInForce,
        decimal quantity,
        OrderStatus status,
        DateTimeOffset createdTimeUtc)
        : base(id)
    {
        Symbol = symbol;
        TradeType = tradeType;
        Type = type;
        TimeInForce = timeInForce;
        Quantity = quantity;
        Status = status;
        CreatedTimeUtc = createdTimeUtc;
    }

    public TradeType TradeType { get; private set; }
    public Symbol Symbol { get; private set; }
    public OrderType Type { get; private set; }
    public TimeInForce TimeInForce { get; private set; }
    public decimal Quantity { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTimeOffset CreatedTimeUtc { get; private set; }

    public static ErrorOr<Order> Create(
        OrderId id,
        Symbol symbol,
        TradeType tradeType,
        OrderType type,
        TimeInForce timeInForce,
        decimal quantity,
        OrderStatus status,
        DateTimeOffset createdTimeUtc)
    {
        var order = new Order(
            id,
            symbol,
            tradeType,
            type,
            timeInForce,
            quantity,
            status,
            createdTimeUtc);

        order.RegisterDomainEvent(new OrderCreatedDomainEvent
        {
            OrderId = order.Id
        });

        return ErrorOr<Order>.With(order);
    }

    public Result Execute(decimal quantity, decimal price)
    {
        Status = OrderStatus.Executed;

        RegisterDomainEvent(new OrderExecutedDomainEvent
        {
            OrderId = Id,
            Symbol = Symbol,
            TradeType = TradeType,
            Quantity = quantity,
            Price = price,
        });

        return Result.Success;
    }
}
