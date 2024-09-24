using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.Domain.Files;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Files.Queries;

internal sealed class GetFileQueryHandler(
    IReadOnlyRepository<FileEntry> _fileRepository) :
    IQueryHandler<GetFileQuery, ErrorOr<FileDto>>
{
    public async Task<ErrorOr<FileDto>> Handle(
        GetFileQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = FileEntryId.From(query.FileId);

        if (validationResult.IsFailure)
        {
            return ErrorOr<FileDto>.WithError(validationResult.Error);
        }

        var id = validationResult.Value;

        var maybeFile = await _fileRepository.GetByIdAsync(id, cancellationToken);

        if (maybeFile.IsNull)
        {
            return ErrorOr<FileDto>.WithError(FileErrors.NotFound(id));
        }

        var file = maybeFile.Value;

        return ErrorOr<FileDto>.With(file.ToDto());
    }
}