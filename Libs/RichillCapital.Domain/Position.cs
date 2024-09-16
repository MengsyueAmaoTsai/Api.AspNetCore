using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Position : Entity<PositionId>
{
    private Position(PositionId id)
        : base(id)
    {
    }

    public static ErrorOr<Position> Create(PositionId id)
    {
        var position = new Position(id);

        return ErrorOr<Position>.With(position);
    }
}
