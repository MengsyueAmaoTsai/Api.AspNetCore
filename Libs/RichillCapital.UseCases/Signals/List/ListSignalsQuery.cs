using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Signals.List;

public sealed record ListSignalsQuery :
    IQuery<ErrorOr<PagedDto<SignalDto>>>
{
}
