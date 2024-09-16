using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class InstrumentType : Enumeration<InstrumentType>
{
    public static readonly InstrumentType Equity = new(nameof(Equity), 1);
    public static readonly InstrumentType Future = new(nameof(Future), 2);
    public static readonly InstrumentType CryptoCurrency = new(nameof(CryptoCurrency), 3);

    private InstrumentType(string name, int value)
        : base(name, value)
    {
    }
}