using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.SignalSources.Queries;

public sealed record ListSignalSourcesQuery :
    IQuery<ErrorOr<IEnumerable<SignalSourceDto>>>
{
}
