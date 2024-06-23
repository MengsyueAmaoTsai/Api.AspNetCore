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
}

public sealed record CreateSignalResponse
{
    public required Guid Id { get; init; }
}

public static class CreateSignalRequestMapping
{
    public static CreateSignalResponse ToResponse(this SignalId id) =>
        new()
        {
            Id = id.Value,
        };

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