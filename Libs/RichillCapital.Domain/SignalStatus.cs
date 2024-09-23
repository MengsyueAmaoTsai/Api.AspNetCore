using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class SignalStatus : Enumeration<SignalStatus>
{
    public static readonly SignalStatus New = new(nameof(New), 1);
    public static readonly SignalStatus Delayed = new(nameof(Delayed), 2);
    public static readonly SignalStatus Blocked = new(nameof(Blocked), 3);
    public static readonly SignalStatus Emitted = new(nameof(Emitted), 4);

    private SignalStatus(string name, int value)
        : base(name, value)
    {
    }
}