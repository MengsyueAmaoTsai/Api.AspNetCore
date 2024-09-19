using RichillCapital.UseCases.Instruments;

namespace RichillCapital.Contracts.Instruments;

public record InstrumentResponse
{
    public required string Symbol { get; init; }
    public required string Description { get; init; }
    public required string Type { get; init; }
    public required decimal ContractUnit { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}

public sealed record InstrumentDetailsResponse : InstrumentResponse
{
}

public static class InstrumentResponseMapping
{
    public static InstrumentResponse ToResponse(this InstrumentDto instrument)
    {
        return new InstrumentResponse
        {
            Symbol = instrument.Symbol,
            Description = instrument.Description,
            Type = instrument.Type,
            ContractUnit = instrument.ContractUnit,
            CreatedTimeUtc = instrument.CreatedTimeUtc,
        };
    }

    public static InstrumentDetailsResponse ToDetailsResponse(this InstrumentDto instrument)
    {
        return new InstrumentDetailsResponse
        {
            Symbol = instrument.Symbol,
            Description = instrument.Description,
            Type = instrument.Type,
            ContractUnit = instrument.ContractUnit,
            CreatedTimeUtc = instrument.CreatedTimeUtc,
        };
    }
}
