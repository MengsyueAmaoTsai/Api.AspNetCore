using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Execution : Entity<ExecutionId>
{
    private Execution(
        ExecutionId id,
        AccountId accountId,
        OrderId orderId,
        Symbol symbol,
        TradeType tradeType,
        OrderType orderType,
        TimeInForce timeInForce,
        decimal quantity,
        decimal price,
        decimal commission,
        decimal tax,
        DateTimeOffset createdTimeUtc)
        : base(id)
    {
        AccountId = accountId;
        OrderId = orderId;
        Symbol = symbol;
        TradeType = tradeType;
        OrderType = orderType;
        TimeInForce = timeInForce;
        Quantity = quantity;
        Price = price;
        Commission = commission;
        Tax = tax;
        CreatedTimeUtc = createdTimeUtc;
    }

    public AccountId AccountId { get; private set; }
    public OrderId OrderId { get; private set; }
    public Symbol Symbol { get; private set; }
    public TradeType TradeType { get; private set; }
    public OrderType OrderType { get; private set; }
    public TimeInForce TimeInForce { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal Price { get; private set; }
    public decimal Commission { get; private set; }
    public decimal Tax { get; private set; }
    public DateTimeOffset CreatedTimeUtc { get; private set; }

    public static ErrorOr<Execution> Create(
        ExecutionId id,
        AccountId accountId,
        OrderId orderId,
        Symbol symbol,
        TradeType tradeType,
        OrderType orderType,
        TimeInForce timeInForce,
        decimal quantity,
        decimal price,
        decimal commission,
        decimal tax,
        DateTimeOffset createdTimeUtc)
    {
        var execution = new Execution(
            id,
            accountId,
            orderId,
            symbol,
            tradeType,
            orderType,
            timeInForce,
            quantity,
            price,
            commission,
            tax,
            createdTimeUtc);

        execution.RegisterDomainEvent(new ExecutionCreatedDomainEvent
        {
            ExecutionId = execution.Id,
        });

        return ErrorOr<Execution>.With(execution);
    }
}