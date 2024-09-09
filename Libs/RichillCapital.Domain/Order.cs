using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class Order : Entity<OrderId>
{
    private Order(
        OrderId id,
        TradeType tradeType,
        Symbol symbol,
        OrderType type,
        TimeInForce timeInForce,
        decimal quantity,
        OrderStatus status,
        DateTimeOffset createdTimeUtc)
        : base(id)
    {
        TradeType = tradeType;
        Symbol = symbol;
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
}
