using RichillCapital.Domain.DataFeeds;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;
using RichillCapital.UseCases.Instruments;

namespace RichillCapital.UseCases.DataFeeds.Queries;

internal sealed class ListDataFeedInstrumentsQueryHandler(
    IDataFeedManager _dataFeedManager) :
    IQueryHandler<ListDataFeedInstrumentsQuery, ErrorOr<IEnumerable<InstrumentDto>>>
{
    public async Task<ErrorOr<IEnumerable<InstrumentDto>>> Handle(
        ListDataFeedInstrumentsQuery query,
        CancellationToken cancellationToken)
    {
        var dataFeedResult = _dataFeedManager.GetByName(query.ConnectionName);

        if (dataFeedResult.IsFailure)
        {
            return ErrorOr<IEnumerable<InstrumentDto>>.WithError(dataFeedResult.Error);
        }

        var dataFeed = dataFeedResult.Value;

        var instrumentsResult = await dataFeed.ListInstrumentsAsync(cancellationToken);

        if (instrumentsResult.IsFailure)
        {
            return ErrorOr<IEnumerable<InstrumentDto>>.WithError(instrumentsResult.Error);
        }

        var instruments = instrumentsResult.Value;

        return ErrorOr<IEnumerable<InstrumentDto>>.With(instruments
            .Select(ins => ins.ToDto())
            .ToList());
    }
}