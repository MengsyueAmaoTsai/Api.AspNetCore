namespace RichillCapital.UseCases.Brokerages;

public sealed record BrokerageDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}