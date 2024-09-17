using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Position : Entity<PositionId>
{
    private Position(
        PositionId id,
        AccountId accountId,
        Symbol symbol,
        Side side,
        decimal quantity,
        decimal averagePrice,
        decimal commission,
        decimal tax,
        decimal swap,
        DateTimeOffset createdTimeUtc)
        : base(id)
    {
        AccountId = accountId;
        Symbol = symbol;
        Side = side;
        Quantity = quantity;
        AveragePrice = averagePrice;
        Commission = commission;
        Tax = tax;
        Swap = swap;
        CreatedTimeUtc = createdTimeUtc;
    }

    public AccountId AccountId { get; private set; }
    public Symbol Symbol { get; private set; }
    public Side Side { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal AveragePrice { get; private set; }
    public decimal Commission { get; private set; }
    public decimal Tax { get; private set; }
    public decimal Swap { get; private set; }
    public DateTimeOffset CreatedTimeUtc { get; private set; }

    public static ErrorOr<Position> Create(
        PositionId id,
        AccountId accountId,
        Symbol symbol,
        Side side,
        decimal quantity,
        decimal averagePrice,
        decimal commission,
        decimal tax,
        decimal swap,
        DateTimeOffset createdTimeUtc)
    {
        var position = new Position(
            id,
            accountId,
            symbol,
            side,
            quantity,
            averagePrice,
            commission,
            tax,
            swap,
            createdTimeUtc);

        position.RegisterDomainEvent(new PositionCreatedDomainEvent
        {
            PositionId = position.Id,
        });

        return ErrorOr<Position>.With(position);
    }

    public Result Update(
        decimal quantity,
        decimal averagePrice,
        decimal commission,
        decimal tax,
        decimal swap)
    {
        Quantity = quantity;
        AveragePrice = averagePrice;
        Commission = commission;
        Tax = tax;
        Swap = swap;

        RegisterDomainEvent(new PositionUpdatedDomainEvent
        {
            PositionId = Id,
        });

        return Result.Success;
    }
}
