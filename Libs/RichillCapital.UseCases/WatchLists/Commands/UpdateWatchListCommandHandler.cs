using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.WatchLists.Commands;

internal sealed class UpdateWatchListCommandHandler(
    IRepository<WatchList> _watchListRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<UpdateWatchListCommand, ErrorOr<WatchListDto>>
{
    public async Task<ErrorOr<WatchListDto>> Handle(
        UpdateWatchListCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = WatchListId.From(command.WatchListId);

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

        var updateResult = list.Update(command.Name);

        if (updateResult.IsFailure)
        {
            return ErrorOr<WatchListDto>.WithError(updateResult.Error);
        }

        _watchListRepository.Update(list);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ErrorOr<WatchListDto>.With(list.ToDto());
    }
}