using Microsoft.AspNetCore.Mvc;

using RichillCapital.UseCases.Files.Get;

namespace RichillCapital.Contracts.Files;

public sealed record GetFileEntryByIdRequest
{
    [FromRoute(Name = "fileId")]
    public required Guid FileId { get; set; }
}

public static class GetFileEntryByIdRequestMapping
{
    public static GetFileEntryByIdQuery ToQuery(this GetFileEntryByIdRequest request) =>
        new()
        {
            FileId = request.FileId,
        };
}