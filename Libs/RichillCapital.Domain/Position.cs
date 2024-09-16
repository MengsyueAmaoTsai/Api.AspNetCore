using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Position : Entity<PositionId>
{
    private Position(
        PositionId id,
        Symbol symbol,
        PositionSide side,
        decimal quantity,
        decimal averagePrice)
        : base(id)
    {
        Symbol = symbol;
        Side = side;
        Quantity = quantity;
        AveragePrice = averagePrice;
    }

    public Symbol Symbol { get; private set; }
    public PositionSide Side { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal AveragePrice { get; private set; }

    public static ErrorOr<Position> Create(
        PositionId id,
        Symbol symbol,
        PositionSide side,
        decimal quantity,
        decimal averagePrice)
    {
        var position = new Position(
            id,
            symbol,
            side,
            quantity,
            averagePrice);

        position.RegisterDomainEvent(
            new PositionCreatedDomainEvent
            {
                PositionId = id,
            });

        return ErrorOr<Position>.With(position);
    }
}
