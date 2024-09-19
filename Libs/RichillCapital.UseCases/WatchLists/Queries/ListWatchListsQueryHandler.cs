using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.WatchLists.Queries;

internal sealed class ListWatchListsQueryHandler(
    IReadOnlyRepository<WatchList> _watchListRepository) :
    IQueryHandler<ListWatchListsQuery, ErrorOr<IEnumerable<WatchListDto>>>
{
    public async Task<ErrorOr<IEnumerable<WatchListDto>>> Handle(
        ListWatchListsQuery query,
        CancellationToken cancellationToken)
    {
        var lists = await _watchListRepository.ListAsync(cancellationToken);

        return ErrorOr<IEnumerable<WatchListDto>>.With(lists
            .Select(l => l.ToDto())
            .ToList());
    }
}