using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Files.List;

public sealed record ListFilesQuery :
    IQuery<ErrorOr<IEnumerable<FileEntryDto>>>
{
}
