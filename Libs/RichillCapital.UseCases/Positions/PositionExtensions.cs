using RichillCapital.Domain;

namespace RichillCapital.UseCases.Positions;

internal static class PositionExtensions
{
    internal static PositionDto ToDto(this Position position) =>
        new()
        {
            Id = position.Id.Value,
            AccountId = position.AccountId.Value,
            Symbol = position.Symbol.Value,
            Side = position.Side.ToString(),
            Quantity = position.Quantity,
            AveragePrice = position.AveragePrice,
            Commission = position.Commission,
            Tax = position.Tax,
            Swap = position.Swap,
            CreatedTimeUtc = position.CreatedTimeUtc,
        };
}