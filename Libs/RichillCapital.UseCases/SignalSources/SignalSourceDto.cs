using RichillCapital.Domain;

namespace RichillCapital.UseCases.SignalSources;

public sealed record SignalSourceDto
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}

internal static class SignalSourceExtensions
{
    public static SignalSourceDto ToDto(this SignalSource source) =>
        new()
        {
            Id = source.Id.Value,
            Name = source.Name,
            Description = source.Description
        };
}