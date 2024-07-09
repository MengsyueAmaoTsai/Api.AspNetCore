using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Instruments.List;

internal sealed class ListInstrumentsQueryHandler(
    IReadOnlyRepository<Instrument> _instrumentRepository) :
    IQueryHandler<ListInstrumentsQuery, ErrorOr<PagedDto<InstrumentDto>>>
{
    public async Task<ErrorOr<PagedDto<InstrumentDto>>> Handle(
        ListInstrumentsQuery query,
        CancellationToken cancellationToken)
    {
        var instruments = await _instrumentRepository.ListAsync(cancellationToken);


        var dto = new PagedDto<InstrumentDto>
        {
            Items = instruments.Select(ins => ins.ToDto()),
            Page = 1,
            PageSize = instruments.Count(),
            TotalCount = instruments.Count(),
        };

        return ErrorOr<PagedDto<InstrumentDto>>.With(dto);
    }
}
