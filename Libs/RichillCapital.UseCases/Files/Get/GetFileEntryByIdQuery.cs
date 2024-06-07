
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Files.Get;

public sealed record GetFileEntryByIdQuery :
    IQuery<ErrorOr<FileEntryDto>>
{
    public required Guid FileId { get; init; }
}
