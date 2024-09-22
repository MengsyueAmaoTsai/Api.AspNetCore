using RichillCapital.Domain;

namespace RichillCapital.UseCases.Signals;

internal static class SignalExtensions
{
    internal static SignalDto ToDto(this Signal signal) =>
        new()
        {
            Id = signal.Id.Value,
        };
}