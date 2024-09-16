using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class PositionSide : Enumeration<PositionSide>
{
    public static readonly PositionSide Long = new(nameof(Long), 1);
    public static readonly PositionSide Short = new(nameof(Short), 2);

    private PositionSide(string name, int value)
        : base(name, value)
    {
    }
}