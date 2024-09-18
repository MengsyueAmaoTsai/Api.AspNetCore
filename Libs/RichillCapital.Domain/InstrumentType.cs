using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class InstrumentType : Enumeration<InstrumentType>
{
    public static readonly InstrumentType Equity = new(nameof(Equity), 1);
    public static readonly InstrumentType Future = new(nameof(Future), 2);
    public static readonly InstrumentType Option = new(nameof(Option), 3);
    public static readonly InstrumentType Index = new(nameof(Index), 4);
    public static readonly InstrumentType ForeX = new(nameof(ForeX), 5);
    public static readonly InstrumentType CryptoCurrency = new(nameof(CryptoCurrency), 6);
    public static readonly InstrumentType Swap = new(nameof(Swap), 7);

    private InstrumentType(string name, int value)
        : base(name, value)
    {
    }
}