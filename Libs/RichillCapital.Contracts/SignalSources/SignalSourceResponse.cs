using RichillCapital.UseCases.Common;
using RichillCapital.UseCases.SignalSources;

namespace RichillCapital.Contracts.SignalSources;

public sealed record SignalSourceResponse
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }
}

public static class SignalSourceResponseMapping
{
    public static SignalSourceResponse ToResponse(this SignalSourceDto source) =>
        new()
        {
            Id = source.Id,
            Name = source.Name,
            Description = source.Description,
            CreatedAt = source.CreatedAt,
        };

    public static Paged<SignalSourceResponse> ToPagedResponse(this PagedDto<SignalSourceDto> paged) =>
        new()
        {
            Items = paged.Items
                .Select(ToResponse),
        };
}