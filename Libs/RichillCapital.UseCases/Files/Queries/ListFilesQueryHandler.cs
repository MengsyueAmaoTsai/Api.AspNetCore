using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Files.Queries;

internal sealed class ListFilesQueryHandler(
    IFileStorageManager _fileStorageManager) :
    IQueryHandler<ListFilesQuery, ErrorOr<IEnumerable<FileDto>>>
{
    public async Task<ErrorOr<IEnumerable<FileDto>>> Handle(
        ListFilesQuery query,
        CancellationToken cancellationToken)
    {
        return ErrorOr<IEnumerable<FileDto>>.With([]);
    }
}