using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Signals.Queries;

internal sealed class ListSignalsQueryHandler(
    IReadOnlyRepository<Signal> _signalRepository) :
    IQueryHandler<ListSignalsQuery, ErrorOr<IEnumerable<SignalDto>>>
{
    public async Task<ErrorOr<IEnumerable<SignalDto>>> Handle(
        ListSignalsQuery query,
        CancellationToken cancellationToken)
    {
        var signals = await _signalRepository.ListAsync(cancellationToken);

        return ErrorOr<IEnumerable<SignalDto>>.With(signals
            .Select(s => s.ToDto())
            .ToList());
    }
}