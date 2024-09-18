using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class Side : Enumeration<Side>
{
    public static readonly Side Long = new(nameof(Long), 1);
    public static readonly Side Short = new(nameof(Short), 2);

    private Side(string name, int value)
        : base(name, value)
    {
    }

    public bool HasSameDirectionAs(TradeType tradeType) =>
        this == Long && tradeType == TradeType.Buy || this == Short && tradeType == TradeType.Sell;

    public Side Reverse() => this == Long ? Short : Long;
}