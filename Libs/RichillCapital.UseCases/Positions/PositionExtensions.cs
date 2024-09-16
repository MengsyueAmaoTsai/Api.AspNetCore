using RichillCapital.Domain;

namespace RichillCapital.UseCases.Positions;

internal static class PositionExtensions
{
    internal static PositionDto ToDto(this Position position) =>
        new()
        {
            Id = position.Id.Value,
            Symbol = position.Symbol.Value,
            Side = position.Side.Name,
            Quantity = position.Quantity,
            AveragePrice = position.AveragePrice,
            Status = position.Status.Name,
            CreatedTimeUtc = position.CreatedTimeUtc,
        };
}