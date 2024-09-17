using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class Side : Enumeration<Side>
{
    public static readonly Side Buy = new(nameof(Buy), 1);
    public static readonly Side Sell = new(nameof(Sell), 2);

    private Side(string name, int value) : base(name, value)
    {
    }
}