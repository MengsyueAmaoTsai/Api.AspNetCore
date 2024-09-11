using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Instrument : Entity<Symbol>
{
    private Instrument(
        Symbol symbol,
        string description)
        : base(symbol)
    {
        Description = description;
    }

    public Symbol Symbol => Id;

    public string Description { get; private set; }

    public static ErrorOr<Instrument> Create(Symbol symbol, string description)
    {
        var instrument = new Instrument(symbol, description);

        return ErrorOr<Instrument>.With(instrument);
    }
}