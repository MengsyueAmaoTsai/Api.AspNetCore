using RichillCapital.Domain;

namespace RichillCapital.UseCases.SignalSources;

internal static class SignalSourceExtensions
{
    internal static SignalSourceDto ToDto(this SignalSource source) =>
        new()
        {
            Id = source.Id.Value,
            Name = source.Name,
            Description = source.Description,
            CreatedTimeUtc = source.CreatedTimeUtc,
        };
}