using RichillCapital.UseCases.Signals.Create;

namespace RichillCapital.Contracts.Signals;

public sealed record CreateSignalRequest
{
    public required string SourceId { get; init; }

    public required DateTimeOffset Time { get; init; }

    public required string Exchange { get; init; }

    public required string Symbol { get; init; }

    public required decimal Quantity { get; init; }

    public required decimal Price { get; init; }

    public required CandleInfo Candle { get; init; }
}

public sealed record CandleInfo
{
    public required DateTimeOffset Time { get; init; }
    public required decimal Open { get; init; }
    public required decimal High { get; init; }
    public required decimal Low { get; init; }
    public required decimal Close { get; init; }
    public required decimal Volume { get; init; }
}

public static class CreateSignalRequestMapping
{
    public static CreateSignalCommand ToCommand(this CreateSignalRequest request) =>
        new()
        {
            SourceId = request.SourceId,
            Time = request.Time,
            Exchange = request.Exchange,
            Symbol = request.Symbol,
            Quantity = request.Quantity,
            Price = request.Price,
        };
}