using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class PositionStatus :
    Enumeration<PositionStatus>
{
    public static readonly PositionStatus Open = new(nameof(Open), 1);
    public static readonly PositionStatus Closed = new(nameof(Closed), 2);

    private PositionStatus(string name, int value)
        : base(name, value)
    {
    }
}