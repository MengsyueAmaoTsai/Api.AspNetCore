using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.DataFeeds.Queries;

public sealed record GetDataFeedQuery :
    IQuery<ErrorOr<DataFeedDto>>
{
    public required string ConnectionName { get; init; }
}
