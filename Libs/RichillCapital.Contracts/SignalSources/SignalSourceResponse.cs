using RichillCapital.UseCases.SignalSources;

namespace RichillCapital.Contracts.SignalSources;

public record SignalSourceResponse
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Visibility { get; init; }
    public required string Status { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}

public sealed record SignalSourceDetailsResponse : SignalSourceResponse
{
}

public static class SignalSourceResponseMapping
{
    public static SignalSourceResponse ToResponse(this SignalSourceDto dto) =>
        new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Visibility = dto.Visibility,
            Status = dto.Status,
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };

    public static SignalSourceDetailsResponse ToDetailsResponse(this SignalSourceDto dto) =>
        new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Visibility = dto.Visibility,
            Status = dto.Status,
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };
}
