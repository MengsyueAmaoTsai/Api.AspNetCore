using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Files.Queries;

public sealed record GetFileQuery : IQuery<ErrorOr<FileDto>>
{
    public required string FileId { get; init; }
}
