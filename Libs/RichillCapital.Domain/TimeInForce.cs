using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class TimeInForce : Enumeration<TimeInForce>
{
    public static readonly TimeInForce Day = new("DAY", 0);
    public static readonly TimeInForce GoodTilCancelled = new("GTC", 1);
    public static readonly TimeInForce ImmediateOrCancel = new("IOC", 2);
    public static readonly TimeInForce FillOrKill = new("FOK", 3);

    private TimeInForce(string name, int value)
        : base(name, value)
    {
    }
}
