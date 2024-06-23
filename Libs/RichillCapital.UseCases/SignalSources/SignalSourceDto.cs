using RichillCapital.Domain;

namespace RichillCapital.UseCases.SignalSources;

public sealed record SignalSourceDto
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }
}

internal static class SignalSourceExtensions
{
    internal static SignalSourceDto ToDto(this SignalSource signalSource) =>
        new()
        {
            Id = signalSource.Id.Value,
            Name = signalSource.Name,
            Description = signalSource.Description,
            CreatedAt = signalSource.CreatedAt,
        };
}
