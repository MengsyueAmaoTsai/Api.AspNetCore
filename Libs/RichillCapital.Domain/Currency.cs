using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class Currency : Enumeration<Currency>
{
    public static readonly Currency TWD = new(nameof(TWD), 0);
    public static readonly Currency USD = new(nameof(USD), 1);
    public static readonly Currency USDT = new(nameof(USDT), 2);
    public static readonly Currency BTC = new(nameof(BTC), 3);

    private Currency(string name, int value)
        : base(name, value)
    {
    }
}