using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class OrderType : Enumeration<OrderType>
{
    public static readonly OrderType Market = new(nameof(Market), 0);
    public static readonly OrderType Limit = new(nameof(Limit), 1);
    public static readonly OrderType Stop = new(nameof(Stop), 2);
    public static readonly OrderType StopLimit = new(nameof(StopLimit), 3);
    public static readonly OrderType TrailingStop = new(nameof(TrailingStop), 4);

    private OrderType(string name, int value)
        : base(name, value)
    {
    }
}
