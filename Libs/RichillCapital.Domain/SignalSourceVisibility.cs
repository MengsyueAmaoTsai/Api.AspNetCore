using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class SignalSourceVisibility : Enumeration<SignalSourceVisibility>
{
    public static readonly SignalSourceVisibility Public = new(nameof(Public), 1);
    public static readonly SignalSourceVisibility Protected = new(nameof(Protected), 2);
    public static readonly SignalSourceVisibility Internal = new(nameof(Internal), 3);
    public static readonly SignalSourceVisibility Private = new(nameof(Private), 4);

    private SignalSourceVisibility(string name, int value)
        : base(name, value)
    {
    }
}