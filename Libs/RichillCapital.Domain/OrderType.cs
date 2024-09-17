using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class OrderType : Enumeration<OrderType>
{
    public static readonly OrderType Market = new(nameof(Market), 1);

    private OrderType(string name, int value)
        : base(name, value)
    {
    }
}
