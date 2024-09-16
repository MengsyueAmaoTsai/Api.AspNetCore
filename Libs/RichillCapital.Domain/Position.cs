using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Position : Entity<PositionId>
{
    private Position(
        PositionId id,
        Symbol symbol)
        : base(id)
    {
        Symbol = symbol;
    }

    public Symbol Symbol { get; private set; }

    public static ErrorOr<Position> Create(
        PositionId id,
        Symbol symbol)
    {
        var position = new Position(
            id,
            symbol);

        position.RegisterDomainEvent(
            new PositionCreatedDomainEvent
            {
                PositionId = id,
            });

        return ErrorOr<Position>.With(position);
    }
}
