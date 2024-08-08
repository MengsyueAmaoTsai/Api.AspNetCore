using RichillCapital.UseCases.SignalSources.Commands;

namespace RichillCapital.Contracts.SignalSources;

public sealed record CreateSignalSourceRequest
{
    public required string Id { get; init; }
}

public static class CreateSignalSourceRequestMapping
{
    public static CreateSignalSourceCommand ToCommand(this CreateSignalSourceRequest request) =>
        new()
        {
            Id = request.Id,
        };
}