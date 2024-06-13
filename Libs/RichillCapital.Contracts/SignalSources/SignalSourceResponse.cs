using RichillCapital.UseCases.Common;
using RichillCapital.UseCases.SignalSources;

namespace RichillCapital.Contracts.SignalSources;

public record SignalSourceResponse
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}

public static class SignalSourceResponseMapping
{
    public static SignalSourceResponse ToSignalSourceResponse(this SignalSourceDto dto) =>
        new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
        };

    public static Paged<SignalSourceResponse> ToPagedResponse(this PagedDto<SignalSourceDto> dto) =>
        new()
        {
            Items = dto.Items.Select(ToSignalSourceResponse),
        };
}