using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Files.List;


internal sealed class ListFilesQueryHandler(
    IReadOnlyRepository<FileEntry> _fileRepository) :
    IQueryHandler<ListFilesQuery, ErrorOr<IEnumerable<FileEntryDto>>>
{
    public async Task<ErrorOr<IEnumerable<FileEntryDto>>> Handle(
        ListFilesQuery query,
        CancellationToken cancellationToken)
    {
        var files = await _fileRepository.ListAsync(cancellationToken);

        return files
            .Select(file => file.ToDto())
            .ToErrorOr();
    }
}