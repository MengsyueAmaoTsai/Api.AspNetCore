using RichillCapital.UseCases.Brokerages;

namespace RichillCapital.Contracts.Brokerages;

public record BrokerageResponse
{
    public required string Name { get; init; }
    public required bool IsConnected { get; init; }
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
            Name = dto.Name,
            IsConnected = dto.IsConnected,
        };
}