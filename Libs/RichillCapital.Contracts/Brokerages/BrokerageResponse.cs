using RichillCapital.UseCases.Brokerages;

namespace RichillCapital.Contracts.Brokerages;

public record BrokerageResponse
{
    public required string Provider { get; init; }
    public required string Name { get; init; }
    public required string Status { get; init; }
}

public sealed record BrokerageDetailsResponse : BrokerageResponse
{
}

public static class BrokerageResponseMapping
{
    public static BrokerageResponse ToResponse(
        this BrokerageDto dto) =>
        new()
        {
            Provider = dto.Provider,
            Name = dto.Name,
            Status = dto.Status,
        };

    public static BrokerageDetailsResponse ToDetailsResponse(
        this BrokerageDto dto) =>
        new()
        {
            Provider = dto.Provider,
            Name = dto.Name,
            Status = dto.Status,
        };
}