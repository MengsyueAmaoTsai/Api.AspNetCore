using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Files;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Files.Queries;

internal sealed class ListFilesQueryHandler(
    IReadOnlyRepository<FileEntry> _fileRepository) :
    IQueryHandler<ListFilesQuery, ErrorOr<IEnumerable<FileDto>>>
{
    public async Task<ErrorOr<IEnumerable<FileDto>>> Handle(
        ListFilesQuery query,
        CancellationToken cancellationToken)
    {
        var files = await _fileRepository.ListAsync(cancellationToken);

        return ErrorOr<IEnumerable<FileDto>>.With(files
            .Select(f => f.ToDto())
            .ToList());
    }
}