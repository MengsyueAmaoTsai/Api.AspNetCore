using RichillCapital.UseCases.Positions;

namespace RichillCapital.Contracts.Positions;

public record PositionResponse
{
    public required string Id { get; init; }
    public required string Symbol { get; init; }
    public required string Side { get; init; }
    public required decimal Quantity { get; init; }
    public required decimal AveragePrice { get; init; }
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
            Symbol = dto.Symbol,
            Side = dto.Side,
            Quantity = dto.Quantity,
            AveragePrice = dto.AveragePrice,
        };

    public static PositionDetailsResponse ToDetailsResponse(this PositionDto dto) =>
        new()
        {
            Id = dto.Id,
            Symbol = dto.Symbol,
            Side = dto.Side,
            Quantity = dto.Quantity,
            AveragePrice = dto.AveragePrice,
        };
}