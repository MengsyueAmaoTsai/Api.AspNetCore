using RichillCapital.UseCases.SignalSources;

namespace RichillCapital.Contracts.SignalSources;

public sealed record SignalSourceDetailsResponse :
    SignalSourceResponse
{
}

public static class SignalSourceDetailsResponseMapping
{
    public static SignalSourceDetailsResponse ToDetailsResponse(this SignalSourceDto dto) =>
        new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
        };
}