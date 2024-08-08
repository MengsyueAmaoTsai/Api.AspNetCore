using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Signals.Queries;

public sealed record GetSignalQuery : IQuery<ErrorOr<SignalDto>>
{
    public required string SignalId { get; init; }
}
