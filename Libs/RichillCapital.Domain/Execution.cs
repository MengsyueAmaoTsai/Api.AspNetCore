using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Execution : Entity<ExecutionId>
{
    private Execution(
        ExecutionId id,
        Symbol symbol,
        TradeType tradeType,
        decimal quantity,
        decimal price,
        DateTimeOffset createdTimeUtc)
        : base(id)
    {
        Symbol = symbol;
        TradeType = tradeType;
        Quantity = quantity;
        Price = price;
        CreatedTimeUtc = createdTimeUtc;
    }

    public Symbol Symbol { get; private set; }
    public TradeType TradeType { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal Price { get; private set; }
    public DateTimeOffset CreatedTimeUtc { get; private set; }

    public static ErrorOr<Execution> Create(
        ExecutionId id,
        Symbol symbol,
        TradeType tradeType,
        decimal quantity,
        decimal price,
        DateTimeOffset createdTimeUtc)
    {
        var execution = new Execution(
            id,
            symbol,
            tradeType,
            quantity,
            price,
            createdTimeUtc);

        return ErrorOr<Execution>.With(execution);
    }
}
