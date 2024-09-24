using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Instrument : Entity<Symbol>
{
    private Instrument(
        Symbol symbol,
        string description,
        InstrumentType type,
        Currency quoteCurrency,
        decimal contractUnit,
        DateTimeOffset createdTimeUtc)
        : base(symbol)
    {
        Description = description;
        Type = type;
        QuoteCurrency = quoteCurrency;
        ContractUnit = contractUnit;
        CreatedTimeUtc = createdTimeUtc;
    }

    public Symbol Symbol => Id;
    public string Description { get; private set; }
    public InstrumentType Type { get; private set; }
    public Currency QuoteCurrency { get; private set; }
    public decimal ContractUnit { get; private set; }

    public DateTimeOffset CreatedTimeUtc { get; private set; }

    public static ErrorOr<Instrument> Create(
        Symbol symbol,
        string description,
        InstrumentType type,
        Currency quoteCurrency,
        decimal contractUnit,
        DateTimeOffset createdTimeUtc)
    {
        var instrument = new Instrument(
            symbol,
            description,
            type,
            quoteCurrency,
            contractUnit,
            createdTimeUtc);

        return ErrorOr<Instrument>.With(instrument);
    }
}
