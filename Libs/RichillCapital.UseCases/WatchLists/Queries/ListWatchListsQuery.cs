using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.WatchLists.Queries;

public sealed record ListWatchListsQuery : IQuery<ErrorOr<IEnumerable<WatchListDto>>>
{
}
