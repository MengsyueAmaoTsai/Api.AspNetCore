
using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.Domain.Common.Storage;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Files.Delete;

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
        var idResult = FileEntryId.From(command.FileId);

        if (idResult.IsFailure)
        {
            return idResult.Error
                .ToResult();
        }

        var maybeFile = await _fileRepository.GetByIdAsync(idResult.Value, cancellationToken);

        if (maybeFile.IsNull)
        {
            return Error
                .NotFound($"File with id {idResult.Value} not found")
                .ToResult();
        }

        var fileEntry = maybeFile.Value;

        await _fileManager.DeleteAsync(fileEntry, cancellationToken);

        _fileRepository.Remove(fileEntry);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}