using RichillCapital.Domain;

namespace RichillCapital.UseCases.SignalSources;

public sealed record SignalSourceDto
{
    public required string Id { get; init; }
}

internal static class SignalSourceExtensions
{
    internal static SignalSourceDto ToDto(this SignalSource signalSource) =>
        new()
        {
            Id = signalSource.Id.Value,
        };
}
