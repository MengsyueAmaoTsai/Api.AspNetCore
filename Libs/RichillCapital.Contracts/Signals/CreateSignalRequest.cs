using RichillCapital.UseCases.Signals.Commands;

namespace RichillCapital.Contracts.Signals;

public sealed record CreateSignalRequest
{
    public required string SourceId { get; init; }
    public required DateTimeOffset Time { get; init; }
    public required string Symbol { get; init; }
}

public static class CreateSignalRequestMapping
{
    public static CreateSignalCommand ToCommand(this CreateSignalRequest request) =>
        new()
        {
            SignalSourceId = request.SourceId,
            Time = request.Time,
            Symbol = request.Symbol,
        };
}