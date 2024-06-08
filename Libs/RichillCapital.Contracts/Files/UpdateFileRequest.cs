using Microsoft.AspNetCore.Mvc;

using RichillCapital.UseCases.Files.Update;

namespace RichillCapital.Contracts.Files;

public sealed record UpdateFileRequest
{
    [FromRoute(Name = "fileId")]
    public required Guid FileId { get; init; }

    [FromBody]
    public required UpdateFileRequestBody Body { get; init; }
}

public sealed record UpdateFileRequestBody
{
    public required string Name { get; init; }

    public required string Description { get; init; }
}

public static class UpdateFileRequestMapping
{
    public static UpdateFileCommand ToCommand(this UpdateFileRequest request) =>
        new()
        {
            Id = request.FileId,
            Name = request.Body.Name,
            Description = request.Body.Description,
        };

}