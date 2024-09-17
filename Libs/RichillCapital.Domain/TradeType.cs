using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class TradeType : Enumeration<TradeType>
{
    public static readonly TradeType Buy = new(nameof(Buy), 1);
    public static readonly TradeType Sell = new(nameof(Sell), 2);

    private TradeType(string name, int value)
        : base(name, value)
    {
    }

    public bool HasSameDirectionAs(Side side) =>
        this == Buy && side == Side.Long || this == Sell && side == Side.Short;
}