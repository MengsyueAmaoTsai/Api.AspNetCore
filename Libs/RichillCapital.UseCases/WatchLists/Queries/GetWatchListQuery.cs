using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.WatchLists.Queries;

public sealed record GetWatchListQuery : IQuery<ErrorOr<WatchListDto>>
{
    public required string WatchListId { get; init; }
}
