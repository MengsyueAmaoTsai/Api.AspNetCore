using Microsoft.AspNetCore.Mvc;

using RichillCapital.UseCases.Files.Update;

namespace RichillCapital.Contracts.Files;

public sealed record UpdateFileEntryRequest
{
    [FromRoute(Name = "fileId")]
    public required Guid FileId { get; init; }

    [FromBody]
    public required UpdateFileEntryRequestBody Body { get; init; }
}

public sealed record UpdateFileEntryRequestBody
{
    public required string Name { get; init; }

    public required string Description { get; init; }
}

public static class UpdateFileEntryRequestMapping
{
    public static UpdateFileEntryCommand ToCommand(this UpdateFileEntryRequest request) =>
        new()
        {
            Id = request.FileId,
            Name = request.Body.Name,
            Description = request.Body.Description,
        };

}