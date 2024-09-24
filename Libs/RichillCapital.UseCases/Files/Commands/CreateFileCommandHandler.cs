using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Files;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Files.Commands;

internal sealed class CreateFileCommandHandler(
    IDateTimeProvider _dateTimeProvider,
    IFileStorageManager _fileStorageManager,
    IRepository<FileEntry> _fileRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<CreateFileCommand, ErrorOr<FileEntryId>>
{
    public async Task<ErrorOr<FileEntryId>> Handle(
        CreateFileCommand command,
        CancellationToken cancellationToken)
    {
        var newId = FileEntryId.NewFileEntryId();
        var fileLocation = _dateTimeProvider.UtcNow.ToString("yyyy-MM-dd/") + newId.Value;

        // TODO: Encrypt the file if the request.Encrypted is true.

        var errorOrFileEntry = FileEntry.Create(
            newId,
            command.Name,
            command.Description,
            size: 0,
            command.FileName,
            fileLocation,
            command.Encrypted,
            encryptionKey: string.Empty,
            encryptionIV: string.Empty,
            DateTimeOffset.UtcNow);

        if (errorOrFileEntry.HasError)
        {
            return ErrorOr<FileEntryId>.WithError(errorOrFileEntry.Errors);
        }

        var file = errorOrFileEntry.Value;

        var createResult = await _fileStorageManager.CreateAsync(file, command.FileStream);

        if (createResult.IsFailure)
        {
            return ErrorOr<FileEntryId>.WithError(createResult.Error);
        }

        _fileRepository.Add(file);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ErrorOr<FileEntryId>.With(file.Id);
    }
}