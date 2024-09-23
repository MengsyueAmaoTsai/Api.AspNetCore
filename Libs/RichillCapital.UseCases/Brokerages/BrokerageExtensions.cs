using RichillCapital.Domain.Brokerages;

namespace RichillCapital.UseCases.Brokerages;

internal static class BrokerageExtensions
{
    internal static BrokerageDto ToDto(
        this IBrokerage brokerage) =>
        new()
        {
            Provider = brokerage.Provider,
            Name = brokerage.Name,
            Status = brokerage.Status.Name,
            Arguments = brokerage.Arguments,
            CreatedTimeUtc = brokerage.CreatedTimeUtc,
        };
}