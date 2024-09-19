using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.WatchLists.Commands;

internal sealed class CreateWatchListCommandHandler(
    IRepository<WatchList> _watchListRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<CreateWatchListCommand, ErrorOr<WatchListId>>
{
    public async Task<ErrorOr<WatchListId>> Handle(
        CreateWatchListCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = UserId.From(command.UserId);

        if (validationResult.IsFailure)
        {
            return ErrorOr<WatchListId>.WithError(validationResult.Error);
        }

        var userId = validationResult.Value;

        var errorOrWatchList = WatchList.Create(
            WatchListId.NewWatchListId(),
            userId,
            command.Name);

        if (errorOrWatchList.HasError)
        {
            return ErrorOr<WatchListId>.WithError(errorOrWatchList.Errors);
        }

        var watchList = errorOrWatchList.Value;

        _watchListRepository.Add(watchList);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ErrorOr<WatchListId>.With(watchList.Id);
    }
}