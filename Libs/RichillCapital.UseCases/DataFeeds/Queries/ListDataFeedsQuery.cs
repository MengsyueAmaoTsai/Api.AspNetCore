using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.DataFeeds.Queries;

public sealed record ListDataFeedsQuery :
    IQuery<ErrorOr<IEnumerable<DataFeedDto>>>
{
}
