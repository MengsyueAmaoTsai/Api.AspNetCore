using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class OrderStatus : Enumeration<OrderStatus>
{
    public static readonly OrderStatus New = new(nameof(New), 0);
    public static readonly OrderStatus Rejected = new(nameof(Rejected), 1);
    public static readonly OrderStatus Pending = new(nameof(Pending), 2);
    public static readonly OrderStatus Cancelled = new(nameof(Cancelled), 3);
    public static readonly OrderStatus PartiallyFilled = new(nameof(PartiallyFilled), 4);
    public static readonly OrderStatus Executed = new(nameof(Executed), 5);

    private OrderStatus(string name, int value)
        : base(name, value)
    {
    }
}