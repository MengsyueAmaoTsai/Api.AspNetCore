using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Files.Queries;

public sealed record DownloadFileQuery : IQuery<ErrorOr<(string Name, byte[] Content)>>
{
    public required string FileId { get; init; }
}
