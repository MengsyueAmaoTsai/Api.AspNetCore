using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Instruments.Queries;

internal sealed class ListInstrumentsQueryHandler(
    IReadOnlyRepository<Instrument> _instrumentRepository) :
    IQueryHandler<ListInstrumentsQuery, ErrorOr<IEnumerable<InstrumentDto>>>
{
    public async Task<ErrorOr<IEnumerable<InstrumentDto>>> Handle(
        ListInstrumentsQuery query,
        CancellationToken cancellationToken)
    {
        var instruments = await _instrumentRepository.ListAsync(cancellationToken);

        var result = instruments
            .Select(i => i.ToDto())
            .ToList();

        return ErrorOr<IEnumerable<InstrumentDto>>.With(result);
    }
}