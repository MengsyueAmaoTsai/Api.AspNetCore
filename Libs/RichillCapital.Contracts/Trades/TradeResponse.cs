using RichillCapital.UseCases.Trades;

namespace RichillCapital.Contracts.Trades;

public record TradeResponse
{
    public required string Id { get; init; }
    public required string Symbol { get; init; }
    public required string Side { get; init; }
    public required DateTimeOffset EntryTimeUtc { get; init; }
    public required decimal EntryPrice { get; init; }
    public required DateTimeOffset ExitTimeUtc { get; init; }
    public required decimal ExitPrice { get; init; }
    public required decimal Quantity { get; init; }
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
            EntryTimeUtc = dto.EntryTimeUtc,
            EntryPrice = dto.EntryPrice,
            ExitTimeUtc = dto.ExitTimeUtc,
            ExitPrice = dto.ExitPrice,
            Quantity = dto.Quantity,
        };

    public static TradeDetailsResponse ToDetailsResponse(this TradeDto dto) =>
        new()
        {
            Id = dto.Id,
            Symbol = dto.Symbol,
            Side = dto.Side,
            EntryTimeUtc = dto.EntryTimeUtc,
            EntryPrice = dto.EntryPrice,
            ExitTimeUtc = dto.ExitTimeUtc,
            ExitPrice = dto.ExitPrice,
            Quantity = dto.Quantity,
        };
}