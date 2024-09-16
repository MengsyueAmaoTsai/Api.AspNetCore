using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Instrument : Entity<Symbol>
{
    private Instrument(
        Symbol symbol,
        string description,
        InstrumentType type)
        : base(symbol)
    {
        Description = description;
        Type = type;
    }

    public Symbol Symbol => Id;
    public string Description { get; private set; }
    public InstrumentType Type { get; private set; }

    public static ErrorOr<Instrument> Create(
        Symbol symbol,
        string description,
        InstrumentType type)
    {
        var instrument = new Instrument(
            symbol,
            description,
            type);

        return ErrorOr<Instrument>.With(instrument);
    }
}
