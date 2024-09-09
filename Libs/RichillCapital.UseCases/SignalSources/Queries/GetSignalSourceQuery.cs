using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.SignalSources.Queries;

public sealed record GetSignalSourceQuery : IQuery<ErrorOr<SignalSourceDto>>
{
    public required string SignalSourceId { get; init; }
}
