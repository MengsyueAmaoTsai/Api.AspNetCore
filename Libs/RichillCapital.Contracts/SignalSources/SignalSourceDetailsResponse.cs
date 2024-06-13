using RichillCapital.Contracts.Signals;
using RichillCapital.UseCases.SignalSources;

namespace RichillCapital.Contracts.SignalSources;

public sealed record SignalSourceDetailsResponse :
    SignalSourceResponse
{
    public required IEnumerable<SignalResponse> Signals { get; init; }
}

public static class SignalSourceDetailsResponseMapping
{
    public static SignalSourceDetailsResponse ToDetailsResponse(this SignalSourceDto dto) =>
        new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Signals = dto.Signals.Select(signal => signal.ToResponse()),
        };
}