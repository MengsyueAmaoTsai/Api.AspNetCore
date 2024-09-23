using RichillCapital.Domain.DataFeeds;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.DataFeeds.Queries;

internal sealed class ListDataFeedsQueryHandler(
    IDataFeedManager _DataFeedManager) :
    IQueryHandler<ListDataFeedsQuery, ErrorOr<IEnumerable<DataFeedDto>>>
{
    public Task<ErrorOr<IEnumerable<DataFeedDto>>> Handle(
        ListDataFeedsQuery query,
        CancellationToken cancellationToken)
    {
        var dataFeeds = _DataFeedManager.ListAll();

        return Task.FromResult(
            ErrorOr<IEnumerable<DataFeedDto>>.With(dataFeeds
                .Select(b => b.ToDto())
                .ToList()));
    }
}