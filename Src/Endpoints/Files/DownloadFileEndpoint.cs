using System.Net;
using System.Net.Mime;

using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.Domain.Files;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Files;

[ApiVersion(EndpointVersion.V1)]
public sealed class DownloadFileEndpoint(
    IReadOnlyRepository<FileEntry> _fileRepository,
    IFileStorageManager _fileManager) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult
{
    [HttpGet(ApiRoutes.Files.Download)]
    [SwaggerOperation(
        Summary = "Download a file",
        Description = "Download a file from the server",
        Tags = [ApiTags.Files])]
    public override async Task<ActionResult> HandleAsync(
        [FromRoute(Name = nameof(fileId))] string fileId,
        CancellationToken cancellationToken = default)
    {
        var validationResult = FileEntryId.From(fileId);

        if (validationResult.IsFailure)
        {
            return HandleFailure(validationResult.Error);
        }

        var id = validationResult.Value;

        var maybeFile = await _fileRepository.GetByIdAsync(id, cancellationToken);

        if (maybeFile.IsNull)
        {
            return HandleFailure(FileErrors.NotFound(id));
        }

        var file = maybeFile.Value;

        var rawDataResult = await _fileManager.ReadAsync(file, cancellationToken);

        if (rawDataResult.IsFailure)
        {
            return HandleFailure(rawDataResult.Error);
        }

        var rawData = rawDataResult.Value;

        // TODO: Decryption logic here

        return File(rawData, MediaTypeNames.Application.Octet, WebUtility.HtmlEncode(file.FileName));
    }
}