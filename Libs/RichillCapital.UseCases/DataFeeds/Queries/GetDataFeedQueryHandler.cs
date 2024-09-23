using RichillCapital.Domain.DataFeeds;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.DataFeeds.Queries;

internal sealed class GetDataFeedQueryHandler(
    IDataFeedManager _DataFeedManager) :
    IQueryHandler<GetDataFeedQuery, ErrorOr<DataFeedDto>>
{
    public Task<ErrorOr<DataFeedDto>> Handle(
        GetDataFeedQuery query,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(query.ConnectionName))
        {
            return Task.FromResult(ErrorOr<DataFeedDto>.WithError(Error.Invalid($"{nameof(query.ConnectionName)} is required.")));
        }

        var dataFeedResult = _DataFeedManager.GetByName(query.ConnectionName);

        if (dataFeedResult.IsFailure)
        {
            return Task.FromResult(ErrorOr<DataFeedDto>.WithError(dataFeedResult.Error));
        }

        var dataFeed = dataFeedResult.Value;

        return Task.FromResult(ErrorOr<DataFeedDto>.With(dataFeed.ToDto()));
    }
}