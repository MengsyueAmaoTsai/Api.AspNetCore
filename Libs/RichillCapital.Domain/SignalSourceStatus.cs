using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class SignalSourceStatus : Enumeration<SignalSourceStatus>
{
    public static readonly SignalSourceStatus Draft = new(nameof(Draft), 0);
    public static readonly SignalSourceStatus Acceptance = new(nameof(Acceptance), 1);
    public static readonly SignalSourceStatus Deployed = new(nameof(Deployed), 2);
    public static readonly SignalSourceStatus Adjusting = new(nameof(Adjusting), 2);
    public static readonly SignalSourceStatus Deprecated = new(nameof(Deprecated), 100);

    private SignalSourceStatus(string name, int value)
        : base(name, value)
    {
    }
}
