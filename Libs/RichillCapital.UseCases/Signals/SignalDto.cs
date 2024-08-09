using RichillCapital.Domain;

namespace RichillCapital.UseCases.Signals;

public sealed record SignalDto
{
    public required string Id { get; init; }
    public required DateTimeOffset Time { get; init; }
    public required string Symbol { get; init; }
}

internal static class SignalExtensions
{
    internal static SignalDto ToDto(this Signal signal) =>
        new()
        {
            Id = signal.Id.Value,
            Time = signal.Time,
            Symbol = signal.Symbol.Value,
        };
}
