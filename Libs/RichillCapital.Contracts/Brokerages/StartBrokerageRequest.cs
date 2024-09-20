namespace RichillCapital.Contracts.Brokerages;

public sealed record StartBrokerageRequest
{
    public required string Provider { get; init; }
    public required string ConnectionName { get; init; }
}

public sealed record StopBrokerageRequest
{
    public required string ConnectionName { get; init; }
}

