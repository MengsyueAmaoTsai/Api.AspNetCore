namespace RichillCapital.Infrastructure.Brokerages;

public sealed record BrokerageProfile
{
    public required string Provider { get; init; }
    public required string Name { get; init; }
    public required bool StartOnBoot { get; init; }
    public required bool Enabled { get; init; }
}
