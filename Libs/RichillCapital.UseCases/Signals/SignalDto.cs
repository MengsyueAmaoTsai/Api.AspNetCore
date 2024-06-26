using RichillCapital.Domain;

namespace RichillCapital.UseCases.Signals;

public class SignalDto
{
    public required Guid Id { get; init; }

    public required string SourceId { get; init; }

    public required DateTimeOffset Time { get; init; }

    public required string Exchange { get; init; }

    public required string Symbol { get; init; }

    public required decimal Quantity { get; init; }

    public required decimal Price { get; init; }

    public required string IpAddress { get; init; }

    public required decimal Latency { get; init; }
}

internal static class SignalExtensions
{
    internal static SignalDto ToDto(this Signal signal) =>
        new()
        {
            Id = signal.Id.Value,
            SourceId = signal.SourceId,
            Time = signal.Time,
            Exchange = signal.Exchange,
            Symbol = signal.Symbol,
            Quantity = signal.Quantity,
            Price = signal.Price,
            IpAddress = signal.IpAddress,
            Latency = signal.Latency,
        };
}