using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Execution : Entity<ExecutionId>
{
    private Execution(
        ExecutionId id,
        OrderId orderId,
        Symbol symbol,
        TradeType tradeType,
        decimal quantity,
        decimal price,
        DateTimeOffset createdTimeUtc)
        : base(id)
    {
        OrderId = orderId;
        Symbol = symbol;
        TradeType = tradeType;
        Quantity = quantity;
        Price = price;
        CreatedTimeUtc = createdTimeUtc;
    }

    public OrderId OrderId { get; private set; }
    public Symbol Symbol { get; private set; }
    public TradeType TradeType { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal Price { get; private set; }
    public DateTimeOffset CreatedTimeUtc { get; private set; }

    public static ErrorOr<Execution> Create(
        ExecutionId id,
        OrderId orderId,
        Symbol symbol,
        TradeType tradeType,
        decimal quantity,
        decimal price,
        DateTimeOffset createdTimeUtc)
    {
        var execution = new Execution(
            id,
            orderId,
            symbol,
            tradeType,
            quantity,
            price,
            createdTimeUtc);

        execution.RegisterDomainEvent(new ExecutionCreatedDomainEvent
        {
            ExecutionId = execution.Id,
        });

        return ErrorOr<Execution>.With(execution);
    }
}
