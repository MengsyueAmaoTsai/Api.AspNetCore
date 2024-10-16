using RichillCapital.Domain;

namespace RichillCapital.UseCases.Instruments;

internal static class InstrumentExtensions
{
    internal static InstrumentDto ToDto(this Instrument instrument) =>
        new()
        {
            Symbol = instrument.Symbol.Value,
            Description = instrument.Description,
            Type = instrument.Type.Name,
            QuoteCurrency = instrument.QuoteCurrency.Name,
            ContractUnit = instrument.ContractUnit,
            CreatedTimeUtc = instrument.CreatedTimeUtc,
        };
}