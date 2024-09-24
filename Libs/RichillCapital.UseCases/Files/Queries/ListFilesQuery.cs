using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Files.Queries;

public sealed record ListFilesQuery : IQuery<ErrorOr<IEnumerable<FileDto>>>
{
}
