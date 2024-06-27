using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Signals.List;

public sealed record ListSignalsQuery :
    IQuery<ErrorOr<PagedDto<SignalDto>>>
{
    public required string SearchTerm { get; init; }

    public required string SortBy { get; init; }
    public required string Order { get; init; }

    public required int Page { get; init; }
    public required int PageSize { get; init; }
}
