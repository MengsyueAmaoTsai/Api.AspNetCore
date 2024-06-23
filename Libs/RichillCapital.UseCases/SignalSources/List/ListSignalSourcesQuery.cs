using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.SignalSources.List;

public sealed record ListSignalSourcesQuery :
    IQuery<ErrorOr<PagedDto<SignalSourceDto>>>
{
}
