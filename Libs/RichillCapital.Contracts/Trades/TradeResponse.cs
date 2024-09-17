using RichillCapital.UseCases.Trades;

namespace RichillCapital.Contracts.Trades;

public record TradeResponse
{
    public required string Id { get; init; }
    public required string AccountId { get; init; }
    public required string Symbol { get; init; }
    public required string Side { get; init; }
    public required decimal Quantity { get; init; }
    public required decimal EntryPrice { get; init; }
    public required DateTimeOffset EntryTimeUtc { get; init; }
    public required decimal ExitPrice { get; init; }
    public required DateTimeOffset ExitTimeUtc { get; init; }
    public required decimal Commission { get; init; }
    public required decimal Tax { get; init; }
    public required decimal Swap { get; init; }
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
            AccountId = dto.AccountId,
            Symbol = dto.Symbol,
            Side = dto.Side,
            Quantity = dto.Quantity,
            EntryPrice = dto.EntryPrice,
            EntryTimeUtc = dto.EntryTimeUtc,
            ExitPrice = dto.ExitPrice,
            ExitTimeUtc = dto.ExitTimeUtc,
            Commission = dto.Commission,
            Tax = dto.Tax,
            Swap = dto.Swap,
        };

    public static TradeDetailsResponse ToDetailsResponse(this TradeDto dto) =>
        new()
        {
            Id = dto.Id,
            AccountId = dto.AccountId,
            Symbol = dto.Symbol,
            Side = dto.Side,
            Quantity = dto.Quantity,
            EntryPrice = dto.EntryPrice,
            EntryTimeUtc = dto.EntryTimeUtc,
            ExitPrice = dto.ExitPrice,
            ExitTimeUtc = dto.ExitTimeUtc,
            Commission = dto.Commission,
            Tax = dto.Tax,
            Swap = dto.Swap,
        };
}