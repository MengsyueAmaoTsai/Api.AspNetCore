
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Files.Get;

public sealed record GetFileByIdQuery :
    IQuery<ErrorOr<FileEntryDto>>
{
}
