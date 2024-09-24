using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Files.Queries;

internal sealed class GetFileQueryHandler(
    IFileStorageManager _fileStorageManager) :
    IQueryHandler<GetFileQuery, ErrorOr<FileDto>>
{
    public Task<ErrorOr<FileDto>> Handle(
        GetFileQuery query,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}