using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class OrderType : Enumeration<OrderType>
{
    public static readonly OrderType Market = new(nameof(Market), 1);
    public static readonly OrderType Limit = new(nameof(Limit), 2);

    private OrderType(string name, int value)
        : base(name, value)
    {
    }
}
