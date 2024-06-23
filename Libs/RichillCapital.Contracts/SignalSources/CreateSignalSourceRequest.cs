using RichillCapital.Domain;
using RichillCapital.UseCases.SignalSources.Create;

namespace RichillCapital.Contracts.SignalSources;

public sealed record CreateSignalSourceRequest
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}

public sealed record CreateSignalSourceResponse
{
    public required string Id { get; init; }
}

public static class CreateSignalSourceRequestMapping
{
    public static CreateSignalSourceCommand ToCommand(this CreateSignalSourceRequest request) =>
        new()
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
        };

    public static CreateSignalSourceResponse ToResponse(this SignalSourceId id) =>
        new()
        {
            Id = id.Value,
        };
}