using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class Currency : Enumeration<Currency>
{
    public static readonly Currency TWD = new(nameof(TWD), 0);
    public static readonly Currency USD = new(nameof(USD), 1);

    private Currency(string name, int value)
        : base(name, value)
    {
    }
}