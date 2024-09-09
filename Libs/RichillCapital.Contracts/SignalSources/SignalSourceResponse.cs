using RichillCapital.UseCases.SignalSources;

namespace RichillCapital.Contracts.SignalSources;

public sealed record SignalSourceResponse
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}

public static class SignalSourceResponseMapping
{
    public static SignalSourceResponse ToResponse(this SignalSourceDto dto) =>
        new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };
}