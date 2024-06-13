using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.SignalSources;

public sealed record ListSignalSourcesQuery :
    IQuery<ErrorOr<PagedDto<SignalSourceDto>>>
{
}
