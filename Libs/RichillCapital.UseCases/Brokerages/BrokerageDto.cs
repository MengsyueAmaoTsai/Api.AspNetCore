using RichillCapital.Domain.Brokerages;

namespace RichillCapital.UseCases.Brokerages;

public sealed record BrokerageDto
{
    public required string Provider { get; init; }
    public required string Name { get; init; }
    public required bool IsConnected { get; init; }
}

internal static class BrokerageExtensions
{
    internal static BrokerageDto ToDto(
        this IBrokerage brokerage) =>
        new()
        {
            Provider = brokerage.Provider,
            Name = brokerage.Name,
            IsConnected = brokerage.IsConnected,
        };
}