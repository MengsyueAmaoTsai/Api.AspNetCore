using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class SignalOrigin :
    Enumeration<SignalOrigin>
{
    public static readonly SignalOrigin Swagger = new(nameof(Swagger), -1);
    public static readonly SignalOrigin VSCodeRestClient = new(nameof(VSCodeRestClient), -2);
    public static readonly SignalOrigin Postman = new(nameof(Postman), -3);

    public static readonly SignalOrigin TradingView = new("RichillCapital.Quant.TradingView", 1);

    private SignalOrigin(string name, int value)
        : base(name, value)
    {
    }
}