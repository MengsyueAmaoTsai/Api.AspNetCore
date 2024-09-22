using RichillCapital.Domain.Brokerages;

namespace RichillCapital.UseCases.Brokerages;

public sealed record BrokerageDto
{
    public required string Provider { get; init; }
    public required string Name { get; init; }
    public required string Status { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}

internal static class BrokerageExtensions
{
    internal static BrokerageDto ToDto(
        this IBrokerage brokerage) =>
        new()
        {
            Provider = brokerage.Provider,
            Name = brokerage.Name,
            Status = brokerage.Status.Name,
            CreatedTimeUtc = brokerage.CreatedTimeUtc,
        };
}