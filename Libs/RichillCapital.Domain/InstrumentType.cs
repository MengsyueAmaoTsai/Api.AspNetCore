using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class InstrumentType : Enumeration<InstrumentType>
{
    public static readonly InstrumentType Equity = new(nameof(Equity), 1);

    private InstrumentType(string name, int value)
        : base(name, value)
    {
    }
}