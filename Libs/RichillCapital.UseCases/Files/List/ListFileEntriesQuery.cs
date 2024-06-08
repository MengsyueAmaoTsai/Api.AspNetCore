using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Files.List;

public sealed record ListFileEntriesQuery :
    IQuery<ErrorOr<IEnumerable<FileEntryDto>>>
{
}
