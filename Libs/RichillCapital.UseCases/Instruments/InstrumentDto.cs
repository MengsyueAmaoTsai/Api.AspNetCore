using RichillCapital.Domain;

namespace RichillCapital.UseCases.Instruments;

public sealed record InstrumentDto
{
    public required string Symbol { get; init; }

    public required string Description { get; init; }
}

internal static class InstrumentExtensions
{
    public static InstrumentDto ToDto(this Instrument instrument) =>
        new()
        {
            Symbol = instrument.Symbol.Value,
            Description = instrument.Description,
        };
}