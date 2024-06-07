using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Files.Get;

internal sealed class GetFileEntryByIdQueryHandler(
    IReadOnlyRepository<FileEntry> _fileRepository) :
    IQueryHandler<GetFileEntryByIdQuery, ErrorOr<FileEntryDto>>
{
    public async Task<ErrorOr<FileEntryDto>> Handle(
        GetFileEntryByIdQuery query,
        CancellationToken cancellationToken)
    {
        var idResult = FileEntryId.From(query.FileId);

        if (idResult.IsFailure)
        {
            return idResult.Error
                .ToErrorOr<FileEntryDto>();
        }

        var maybeFile = await _fileRepository.GetByIdAsync(idResult.Value, cancellationToken);

        if (maybeFile.IsNull)
        {
            return Error
                .NotFound($"File with id {query.FileId} not found")
                .ToErrorOr<FileEntryDto>();
        }

        return maybeFile.Value
            .ToDto()
            .ToErrorOr();
    }
}