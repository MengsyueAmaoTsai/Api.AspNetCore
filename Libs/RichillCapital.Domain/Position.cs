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
        decimal averagePrice,
        PositionStatus status,
        DateTimeOffset createdTimeUtc)
        : base(id)
    {
        Symbol = symbol;
        Side = side;
        Quantity = quantity;
        AveragePrice = averagePrice;
        Status = status;
        CreatedTimeUtc = createdTimeUtc;
    }

    public Symbol Symbol { get; private set; }
    public PositionSide Side { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal AveragePrice { get; private set; }
    public PositionStatus Status { get; private set; }
    public DateTimeOffset CreatedTimeUtc { get; private set; }

    public static ErrorOr<Position> Create(
        PositionId id,
        Symbol symbol,
        PositionSide side,
        decimal quantity,
        decimal averagePrice,
        PositionStatus status,
        DateTimeOffset createdTimeUtc)
    {
        var position = new Position(
            id,
            symbol,
            side,
            quantity,
            averagePrice,
            status,
            createdTimeUtc);

        position.RegisterDomainEvent(
            new PositionCreatedDomainEvent
            {
                PositionId = id,
            });

        return ErrorOr<Position>.With(position);
    }
}

public static class PositionExtensions
{
    public static bool HasSameDirectionAs(this Position position, Execution execution) =>
        position.Side.HasSameDirectionAs(execution.TradeType);
}