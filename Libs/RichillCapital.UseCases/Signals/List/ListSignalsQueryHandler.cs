using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Signals.List;

internal sealed class ListSignalsQueryHandler(
    IReadOnlyRepository<Signal> _signalRepository) :
    IQueryHandler<ListSignalsQuery, ErrorOr<PagedDto<SignalDto>>>
{
    public async Task<ErrorOr<PagedDto<SignalDto>>> Handle(
        ListSignalsQuery query,
        CancellationToken cancellationToken)
    {
        var signals = await _signalRepository.ListAsync(cancellationToken);

        return ErrorOr<PagedDto<SignalDto>>
            .With(new PagedDto<SignalDto>
            {
                Items = signals
                    .Select(signal => signal.ToDto()),
            });
    }
}