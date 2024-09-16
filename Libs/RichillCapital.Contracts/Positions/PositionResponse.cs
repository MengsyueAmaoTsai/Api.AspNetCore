using RichillCapital.UseCases.Positions;

namespace RichillCapital.Contracts.Positions;

public record PositionResponse
{
    public required string Id { get; init; }
}

public sealed record PositionDetailsResponse : PositionResponse
{
}

public static class PositionResponseMapping
{
    public static PositionResponse ToResponse(this PositionDto dto) =>
        new()
        {
            Id = dto.Id,
        };

    public static PositionDetailsResponse ToDetailsResponse(this PositionDto dto) =>
        new()
        {
            Id = dto.Id,
        };
}