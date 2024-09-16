using RichillCapital.UseCases.Positions;

namespace RichillCapital.Contracts.Positions;

public sealed record PositionResponse
{
    public required string Id { get; init; }
}

public static class PositionResponseMapping
{
    public static PositionResponse ToResponse(this PositionDto dto) =>
        new()
        {
            Id = dto.Id,
        };
}