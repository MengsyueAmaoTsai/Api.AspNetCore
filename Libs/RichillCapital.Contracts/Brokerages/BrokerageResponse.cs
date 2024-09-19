using RichillCapital.UseCases.Brokerages;

namespace RichillCapital.Contracts.Brokerages;

public record BrokerageResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}

public sealed record BrokerageDetailsResponse : BrokerageResponse
{
}

public static class BrokerageResponseMapping
{
    public static BrokerageResponse ToResponse(this BrokerageDto dto) =>
        new()
        {
            Id = dto.Id,
            Name = dto.Name
        };
}