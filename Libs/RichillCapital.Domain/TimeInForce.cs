using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class TimeInForce : Enumeration<TimeInForce>
{
    public static readonly TimeInForce ImmediateOrCancel = new("IOC", 1);

    private TimeInForce(string name, int value)
        : base(name, value)
    {
    }
}
