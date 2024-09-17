using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class TimeInForce : Enumeration<TimeInForce>
{
    public static readonly TimeInForce GoodTillCancelled = new("GTC", 1);
    public static readonly TimeInForce ImmediateOrCancel = new("IOC", 2);
    public static readonly TimeInForce FillOrKill = new("FOK", 3);
    public static readonly TimeInForce Day = new("DAY", 4);
    public static readonly TimeInForce GoodTillDate = new("GTD", 5);

    private TimeInForce(string name, int value)
        : base(name, value)
    {
    }
}
