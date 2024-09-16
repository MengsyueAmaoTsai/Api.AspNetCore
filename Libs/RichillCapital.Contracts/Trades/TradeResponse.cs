using RichillCapital.UseCases.Trades;

namespace RichillCapital.Contracts.Trades;

public record TradeResponse
{
    public required string Id { get; init; }
    public required string Symbol { get; init; }
    public required string Side { get; init; }
}

public sealed record TradeDetailsResponse : TradeResponse
{
}

public static class TradeResponseMapping
{
    public static TradeResponse ToResponse(this TradeDto dto) =>
        new()
        {
            Id = dto.Id,
            Symbol = dto.Symbol,
            Side = dto.Side,
        };

    public static TradeDetailsResponse ToDetailsResponse(this TradeDto dto) =>
        new()
        {
            Id = dto.Id,
            Symbol = dto.Symbol,
            Side = dto.Side,
        };
}