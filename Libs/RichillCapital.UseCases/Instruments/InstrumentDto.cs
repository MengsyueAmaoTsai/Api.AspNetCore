namespace RichillCapital.UseCases.Instruments;

public sealed record InstrumentDto
{
    public required string Symbol { get; init; }
    public required string Description { get; init; }
    public required string Type { get; init; }
    public required string QuoteCurrency { get; init; }
    public required decimal ContractUnit { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}