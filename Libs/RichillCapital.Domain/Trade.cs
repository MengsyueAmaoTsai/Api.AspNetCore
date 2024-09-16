using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Trade : Entity<TradeId>
{
    private Trade(
        TradeId id,
        Symbol symbol,
        PositionSide side) : base(id)
    {
        Symbol = symbol;
        Side = side;
    }

    public Symbol Symbol { get; private set; }
    public PositionSide Side { get; private set; }

    public static ErrorOr<Trade> Create(
        TradeId id,
        Symbol symbol,
        PositionSide side)
    {
        var trade = new Trade(
            id,
            symbol,
            side);

        return ErrorOr<Trade>.With(trade);
    }
}
