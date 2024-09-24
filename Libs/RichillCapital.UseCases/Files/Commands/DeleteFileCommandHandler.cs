using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.Domain.Files;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Files.Commands;

internal sealed class DeleteFileCommandHandler(
    IFileStorageManager _fileManager,
    IRepository<FileEntry> _fileRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<DeleteFileCommand, Result>
{
    public async Task<Result> Handle(
        DeleteFileCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = FileEntryId.From(command.FileId);

        if (validationResult.IsFailure)
        {
            return Result.Failure(validationResult.Error);
        }

        var id = validationResult.Value;

        var maybeFile = await _fileRepository.GetByIdAsync(id, cancellationToken);

        if (maybeFile.IsNull)
        {
            return Result.Failure(FileErrors.NotFound(id));
        }

        var file = maybeFile.Value;

        var deleteResult = await _fileManager.DeleteAsync(file, cancellationToken);

        if (deleteResult.IsFailure)
        {
            return Result.Failure(deleteResult.Error);
        }

        _fileRepository.Remove(file);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}