using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Signals.List;

internal sealed class ListSignalsQueryHandler() :
    IQueryHandler<ListSignalsQuery, ErrorOr<PagedDto<SignalDto>>>
{
    public async Task<ErrorOr<PagedDto<SignalDto>>> Handle(
        ListSignalsQuery query,
        CancellationToken cancellationToken)
    {
        return ErrorOr<PagedDto<SignalDto>>
            .With(new PagedDto<SignalDto>
            {
                Items = [],
            });
    }
}