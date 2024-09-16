using RichillCapital.Domain;

namespace RichillCapital.UseCases.Positions;

internal static class PositionExtensions
{
    internal static PositionDto ToDto(this Position position) =>
        new()
        {
            Id = position.Id.Value,
        };
}