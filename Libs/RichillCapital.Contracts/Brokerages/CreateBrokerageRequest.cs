namespace RichillCapital.Contracts.Brokerages;

public sealed record CreateBrokerageRequest
{
    public required string Provider { get; init; }
    public required string Name { get; init; }
}