
using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.WatchLists.Commands;

internal sealed class DeleteWatchListCommandHandler(
    IRepository<WatchList> _watchListRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<DeleteWatchListCommand, Result>
{
    public async Task<Result> Handle(
        DeleteWatchListCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = WatchListId.From(command.WatchListId);

        if (validationResult.IsFailure)
        {
            return Result.Failure(validationResult.Error);
        }

        var id = validationResult.Value;

        var maybeList = await _watchListRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (maybeList.IsNull)
        {
            return Result.Failure(WatchListErrors.NotFound(id));
        }

        var list = maybeList.Value;

        _watchListRepository.Remove(list);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}