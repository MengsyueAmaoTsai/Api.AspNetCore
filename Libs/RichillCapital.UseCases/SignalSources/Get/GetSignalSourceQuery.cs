using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.SignalSources.Get;

public sealed record GetSignalSourceQuery :
    IQuery<ErrorOr<SignalSourceDto>>
{
    public required string SourceId { get; init; }
}
