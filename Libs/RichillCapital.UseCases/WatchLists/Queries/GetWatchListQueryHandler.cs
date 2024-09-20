
using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.WatchLists.Queries;

internal sealed class GetWatchListQueryHandler(
    IReadOnlyRepository<WatchList> _watchListRepository) :
    IQueryHandler<GetWatchListQuery, ErrorOr<WatchListDto>>
{
    public async Task<ErrorOr<WatchListDto>> Handle(
        GetWatchListQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = WatchListId.From(query.WatchListId);

        if (validationResult.IsFailure)
        {
            return ErrorOr<WatchListDto>.WithError(validationResult.Error);
        }

        var id = validationResult.Value;

        var maybeList = await _watchListRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (maybeList.IsNull)
        {
            return ErrorOr<WatchListDto>.WithError(WatchListErrors.NotFound(id));
        }

        var list = maybeList.Value;

        return ErrorOr<WatchListDto>.With(list.ToDto());
    }
}