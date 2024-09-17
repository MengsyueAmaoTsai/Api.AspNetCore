using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class SignalOrigin :
    Enumeration<SignalOrigin>
{
    public static readonly SignalOrigin TradingView = new("RichillCapital.Quant.TradingView", 1);

    private SignalOrigin(string name, int value)
        : base(name, value)
    {
    }
}