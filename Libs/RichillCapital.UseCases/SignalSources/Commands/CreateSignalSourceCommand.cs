
using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.SignalSources.Commands;

public sealed record CreateSignalSourceCommand : ICommand<ErrorOr<SignalSourceId>>
{
    public required string Id { get; init; }
}
