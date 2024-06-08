using Microsoft.AspNetCore.Mvc;

using RichillCapital.UseCases.Files.Delete;

namespace RichillCapital.Contracts.Files;

public sealed record DeleteFileRequest
{
    [FromRoute(Name = "fileId")]
    public required Guid FileId { get; init; }
}

public static class DeleteFileRequestMapping
{
    public static DeleteFileCommand ToCommand(this DeleteFileRequest request) =>
        new()
        {
            FileId = request.FileId,
        };
}