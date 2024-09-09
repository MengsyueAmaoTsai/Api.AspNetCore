using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class SignalOrigin :
    Enumeration<SignalOrigin>
{
    public static readonly SignalOrigin MetaTrader = new("RichillCapital.Quant.MetaTrader", 1);
    public static readonly SignalOrigin CTrader = new("RichillCapital.Quant.CTrader", 2);
    public static readonly SignalOrigin Quantower = new("RichillCapital.Quant.Quantower", 3);
    public static readonly SignalOrigin MultiCharts = new("RichillCapital.Quant.MultiCharts", 4);
    public static readonly SignalOrigin XQ = new("RichillCapital.Quant.XQ", 5);
    public static readonly SignalOrigin MultiChartsNet = new("RichillCapital.Quant.MultiCharts.Net", 6);
    public static readonly SignalOrigin NinjaTrader = new("RichillCapital.Quant.NinjaTrader", 7);
    public static readonly SignalOrigin TradeStation = new("RichillCapital.Quant.TradeStation", 8);
    public static readonly SignalOrigin QuantConnect = new("RichillCapital.Quant.QuantConnect", 9);
    public static readonly SignalOrigin WealthLabe = new("RichillCapital.Quant.WealthLab", 10);
    public static readonly SignalOrigin TradingView = new("RichillCapital.Quant.TradingView", 11);

    private SignalOrigin(string name, int value)
        : base(name, value)
    {
    }
}