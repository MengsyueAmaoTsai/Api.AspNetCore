using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class OrderStatus : Enumeration<OrderStatus>
{
    public static readonly OrderStatus New = new(nameof(New), 1);
    public static readonly OrderStatus Executed = new(nameof(Executed), 2);
    public static readonly OrderStatus PartiallyFilled = new(nameof(PartiallyFilled), 3);
    public static readonly OrderStatus Cancelled = new(nameof(Cancelled), 4);

    private OrderStatus(string name, int value)
        : base(name, value)
    {
    }
}