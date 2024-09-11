using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Instruments.Queries;

internal sealed class GetInstrumentQueryHandler(
    IReadOnlyRepository<Instrument> _instrumentRepository) :
    IQueryHandler<GetInstrumentQuery, ErrorOr<InstrumentDto>>
{
    public async Task<ErrorOr<InstrumentDto>> Handle(
        GetInstrumentQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = Symbol.From(query.Symbol);

        if (validationResult.IsFailure)
        {
            return ErrorOr<InstrumentDto>.WithError(validationResult.Error);
        }

        var symbol = validationResult.Value;

        var maybeInstrument = await _instrumentRepository.FirstOrDefaultAsync(
            i => i.Symbol == symbol,
            cancellationToken);

        if (maybeInstrument.IsNull)
        {
            return ErrorOr<InstrumentDto>.WithError(InstrumentErrors.NotFound(symbol));
        }

        var instrument = maybeInstrument.Value;

        return ErrorOr<InstrumentDto>.With(instrument.ToDto());
    }
}