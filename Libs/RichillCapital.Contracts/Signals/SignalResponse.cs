using RichillCapital.UseCases.Signals;

namespace RichillCapital.Contracts.Signals;

public record SignalResponse
{
    public required string Id { get; init; }
    public required string SourceId { get; init; }
    public required string Origin { get; init; }
    public required string Symbol { get; init; }
    public required DateTimeOffset Time { get; init; }
    public required string TradeType { get; init; }
    public required decimal Quantity { get; init; }
    public required long Latency { get; init; }
    public required string Status { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}

public sealed record SignalDetailsResponse : SignalResponse
{
}

public static class SignalResponseMapping
{
    public static SignalResponse ToResponse(this SignalDto dto) =>
        new()
        {
            Id = dto.Id,
            SourceId = dto.SourceId,
            Origin = dto.Origin,
            Symbol = dto.Symbol,
            Time = dto.Time,
            TradeType = dto.TradeType,
            Quantity = dto.Quantity,
            Latency = dto.Latency,
            Status = dto.Status,
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };

    public static SignalDetailsResponse ToDetailsResponse(this SignalDto dto) =>
        new()
        {
            Id = dto.Id,
            SourceId = dto.SourceId,
            Origin = dto.Origin,
            Symbol = dto.Symbol,
            Time = dto.Time,
            TradeType = dto.TradeType,
            Quantity = dto.Quantity,
            Latency = dto.Latency,
            Status = dto.Status,
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };
}