using RichillCapital.UseCases.Common;
using RichillCapital.UseCases.Signals;

namespace RichillCapital.Contracts.Signals;

public sealed record SignalResponse
{
    public required Guid Id { get; init; }

    public required string SourceId { get; init; }

    public required DateTimeOffset Time { get; init; }

    public required string Exchange { get; init; }

    public required string Symbol { get; init; }

    public required decimal Quantity { get; init; }

    public required decimal Price { get; init; }

    public required decimal Latency { get; init; }
}

public static class SignalResponseMapping
{
    public static SignalResponse ToResponse(this SignalDto dto) =>
        new()
        {
            Id = dto.Id,
            SourceId = dto.SourceId,
            Time = dto.Time,
            Exchange = dto.Exchange,
            Symbol = dto.Symbol,
            Quantity = dto.Quantity,
            Price = dto.Price,
            Latency = dto.Latency,
        };

    public static Paged<SignalResponse> ToPagedResponse(this PagedDto<SignalDto> dto) =>
        new()
        {
            Items = dto.Items.Select(ToResponse),
        };
}