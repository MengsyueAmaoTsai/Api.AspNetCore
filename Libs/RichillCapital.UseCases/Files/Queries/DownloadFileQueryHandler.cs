using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.Domain.Files;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Files.Queries;

internal sealed class DownloadFileQueryHandler(
    IReadOnlyRepository<FileEntry> _fileRepository,
    IFileStorageManager _fileManager) :
    IQueryHandler<DownloadFileQuery, ErrorOr<(string Name, byte[] Content)>>
{
    public async Task<ErrorOr<(string, byte[])>> Handle(
        DownloadFileQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = FileEntryId.From(query.FileId);

        if (validationResult.IsFailure)
        {
            return ErrorOr<(string, byte[])>.WithError(validationResult.Error);
        }

        var id = validationResult.Value;

        var maybeFile = await _fileRepository.GetByIdAsync(id, cancellationToken);

        if (maybeFile.IsNull)
        {
            return ErrorOr<(string, byte[])>.WithError(FileErrors.NotFound(id));
        }

        var file = maybeFile.Value;

        var rawDataResult = await _fileManager.ReadAsync(file, cancellationToken);

        if (rawDataResult.IsFailure)
        {
            return ErrorOr<(string, byte[])>.WithError(rawDataResult.Error);
        }

        var rawData = rawDataResult.Value;

        return ErrorOr<(string, byte[])>.With((file.Name, rawData));
    }
}