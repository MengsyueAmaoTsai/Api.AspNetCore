using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Order : Entity<OrderId>
{
    private readonly List<Execution> _executions = [];

    private Order(
        OrderId id,
        AccountId accountId,
        Symbol symbol,
        TradeType tradeType,
        OrderType type,
        TimeInForce timeInForce,
        decimal quantity,
        decimal remainingQuantity,
        decimal executedQuantity,
        OrderStatus status,
        string clientOrderId,
        DateTimeOffset createdTimeUtc)
        : base(id)
    {
        AccountId = accountId;
        Symbol = symbol;
        TradeType = tradeType;
        Type = type;
        TimeInForce = timeInForce;
        Quantity = quantity;
        RemainingQuantity = remainingQuantity;
        ExecutedQuantity = executedQuantity;
        Status = status;
        ClientOrderId = clientOrderId;
        CreatedTimeUtc = createdTimeUtc;
    }

    public AccountId AccountId { get; private set; }
    public Symbol Symbol { get; private set; }
    public TradeType TradeType { get; private set; }
    public OrderType Type { get; private set; }
    public TimeInForce TimeInForce { get; private set; }
    public decimal Quantity { get; private init; }
    public decimal RemainingQuantity { get; private set; }
    public decimal ExecutedQuantity { get; private set; }
    public OrderStatus Status { get; private set; }
    public string ClientOrderId { get; private set; }
    public DateTimeOffset CreatedTimeUtc { get; private set; }

    public IReadOnlyCollection<Execution> Executions => _executions;

    public static ErrorOr<Order> Create(
        OrderId id,
        AccountId accountId,
        Symbol symbol,
        TradeType tradeType,
        OrderType type,
        TimeInForce timeInForce,
        decimal quantity,
        decimal remainingQuantity,
        decimal executedQuantity,
        OrderStatus status,
        string clientOrderId,
        DateTimeOffset createdTimeUtc)
    {
        var order = new Order(
            id,
            accountId,
            symbol,
            tradeType,
            type,
            timeInForce,
            quantity,
            remainingQuantity,
            executedQuantity,
            status,
            clientOrderId,
            createdTimeUtc);

        order.RegisterDomainEvent(new OrderCreatedDomainEvent
        {
            AccountId = accountId,
            OrderId = id,
            Symbol = symbol,
            TradeType = tradeType,
            OrderType = type,
            TimeInForce = timeInForce,
            Quantity = quantity,
            ExecutedQuantity = executedQuantity,
            RemainingQuantity = remainingQuantity,
            Status = status,
            ClientOrderId = clientOrderId,
            CreatedTimeUtc = createdTimeUtc,
        });

        return ErrorOr<Order>.With(order);
    }

    public Result Reject(string reason)
    {
        Status = OrderStatus.Rejected;

        RegisterDomainEvent(new OrderRejectedDomainEvent
        {
            OrderId = Id,
            AccountId = AccountId,
            Symbol = Symbol,
            TradeType = TradeType,
            OrderType = Type,
            TimeInForce = TimeInForce,
            Quantity = Quantity,
            ExecutedQuantity = ExecutedQuantity,
            RemainingQuantity = RemainingQuantity,
            Status = Status,
            ClientOrderId = ClientOrderId,
            CreatedTimeUtc = CreatedTimeUtc,
            Reason = reason,
        });

        return Result.Success;
    }

    public Result Accept()
    {
        Status = OrderStatus.Pending;

        RegisterDomainEvent(new OrderAcceptedDomainEvent
        {
            OrderId = Id,
            AccountId = AccountId,
            Symbol = Symbol,
            TradeType = TradeType,
            OrderType = Type,
            TimeInForce = TimeInForce,
            Quantity = Quantity,
            ExecutedQuantity = ExecutedQuantity,
            RemainingQuantity = RemainingQuantity,
            Status = Status,
            ClientOrderId = ClientOrderId,
            CreatedTimeUtc = CreatedTimeUtc,
        });

        return Result.Success;
    }

    public Result Cancel()
    {
        Status = OrderStatus.Cancelled;

        RegisterDomainEvent(new OrderCancelledDomainEvent
        {
            OrderId = Id,
            AccountId = AccountId,
            Symbol = Symbol,
            TradeType = TradeType,
            OrderType = Type,
            TimeInForce = TimeInForce,
            Quantity = Quantity,
            ExecutedQuantity = ExecutedQuantity,
            RemainingQuantity = RemainingQuantity,
            Status = Status,
            ClientOrderId = ClientOrderId,
            CreatedTimeUtc = CreatedTimeUtc,
        });

        return Result.Success;
    }

    public Result Execute(
        decimal quantity,
        decimal price)
    {
        if (quantity > RemainingQuantity)
        {
            return Result.Failure(Error.Conflict("Quantity to execute is greater than remaining quantity"));
        }

        RemainingQuantity -= quantity;
        ExecutedQuantity += quantity;

        if (RemainingQuantity > 0)
        {
            Status = OrderStatus.PartiallyFilled;
        }
        else
        {
            Status = OrderStatus.Executed;
        }

        RegisterDomainEvent(new OrderExecutedDomainEvent
        {
            OrderId = Id,
            AccountId = AccountId,
            Symbol = Symbol,
            TradeType = TradeType,
            OrderType = Type,
            TimeInForce = TimeInForce,
            Quantity = Quantity,
            ExecutedQuantity = ExecutedQuantity,
            RemainingQuantity = RemainingQuantity,
            Status = Status,
            ClientOrderId = ClientOrderId,
            CreatedTimeUtc = CreatedTimeUtc,
            Price = price,
        });

        return Result.Success;
    }
}
