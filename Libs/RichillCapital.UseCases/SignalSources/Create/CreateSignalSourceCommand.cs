using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.SignalSources.Create;

public sealed record CreateSignalSourceCommand :
    ICommand<ErrorOr<SignalSourceId>>
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}
