using RichillCapital.UseCases.SignalSources;

namespace RichillCapital.Contracts.SignalSources;

public sealed record SignalSourceDetailsResponse 
{
    public required string Id { get; init; }
}

public static class SignalSourceDetailsResponseMapping
{
    public static SignalSourceDetailsResponse ToDetailsResponse(this SignalSourceDto dto) =>
        new()
        {
            Id = dto.Id,
        };
}