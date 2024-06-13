using RichillCapital.UseCases.Signals;

namespace RichillCapital.Contracts.Signals;

public sealed record SignalResponse
{
    public required string Id { get; init; }
}

internal static class SignalExtensions
{
    public static SignalResponse ToResponse(this SignalDto signal) =>
        new()
        {
            Id = signal.Id,
        };
}