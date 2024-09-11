using RichillCapital.Domain;

namespace RichillCapital.UseCases.Instruments;

internal static class InstrumentExtensions
{
    internal static InstrumentDto ToDto(this Instrument instrument) =>
        new()
        {
            Symbol = instrument.Symbol.Value,
            Description = instrument.Description,
        };
}