
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Files.Get;

internal sealed class GetFileByIdQueryHandler() :
    IQueryHandler<GetFileByIdQuery, ErrorOr<FileEntryDto>>
{
    public Task<ErrorOr<FileEntryDto>> Handle(
        GetFileByIdQuery query,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}