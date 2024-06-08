using Microsoft.AspNetCore.Mvc;

using RichillCapital.UseCases.Files.Download;

namespace RichillCapital.Contracts.Files;

public sealed record DownloadFileRequest
{
    [FromRoute(Name = "fileId")]
    public required Guid FileId { get; init; }
}

public static class DownloadFileRequestMapping
{
    public static DownloadFileQuery ToQuery(this DownloadFileRequest request) =>
        new()
        {
            FileId = request.FileId,
        };
}