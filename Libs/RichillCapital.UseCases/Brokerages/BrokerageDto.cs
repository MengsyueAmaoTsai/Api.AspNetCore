namespace RichillCapital.UseCases.Brokerages;

public sealed record BrokerageDto
{
    public required string Provider { get; init; }
    public required string Name { get; init; }
    public required string Status { get; init; }
    public required IReadOnlyDictionary<string, object> Arguments { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}
