using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;
using RichillCapital.UseCases.Files;

namespace RichillCapital.UseCases.Files.Download;

public sealed record DownloadFileQuery :
    IQuery<ErrorOr<FileDto>>
{
    public required Guid FileId { get; init; }
}
