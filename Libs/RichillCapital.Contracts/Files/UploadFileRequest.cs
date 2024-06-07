using Microsoft.AspNetCore.Http;

using RichillCapital.UseCases.Files.Upload;

namespace RichillCapital.Contracts.Files;

public sealed record UploadFileRequest
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required IFormFile File { get; init; }
    public required bool Encrypted { get; init; }
}

public static class UploadFileRequestMapping
{
    public static UploadFileCommand ToCommand(this UploadFileRequest request) =>
        new()
        {
            Name = request.Name,
            Description = request.Description,
            Size = request.File.Length,
            UploadedTime = DateTimeOffset.UtcNow,
            FileName = request.File.FileName,
            Encrypted = request.Encrypted,
        };
}