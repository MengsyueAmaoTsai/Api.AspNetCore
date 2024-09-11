namespace RichillCapital.UseCases.Instruments;

public sealed record InstrumentDto
{
    public required string Symbol { get; init; }
    public required string Description { get; init; }
}