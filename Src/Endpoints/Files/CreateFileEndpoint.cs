using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Files;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Files;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Files;

public sealed class CreateFileEndpoint(
    IDateTimeProvider _dateTimeProvider,
    IRepository<FileEntry> _fileRepository,
    IFileStorageManager _fileManager,
    IUnitOfWork _unitOfWork) : AsyncEndpoint
    .WithRequest<CreateFileRequest>
    .WithActionResult<FileCreatedResponse>
{
    [HttpPost(ApiRoutes.Files.Create)]
    [SwaggerOperation(
        Summary = "Creates a new file.",
        Description = "Creates a new file in the file storage system.",
        Tags = [ApiTags.Files])]
    public override async Task<ActionResult<FileCreatedResponse>> HandleAsync(
        [FromForm] CreateFileRequest request,
        CancellationToken cancellationToken = default)
    {
        var newId = FileEntryId.NewFileEntryId();
        var fileLocation = _dateTimeProvider.UtcNow.ToString("yyyy-MM-dd/") + newId.Value;

        // TODO: Encrypt the file if the request.Encrypted is true.
        var errorOrFileEntry = FileEntry.Create(
            newId,
            request.Name,
            request.Description,
            size: 0,
            request.FromFile.FileName,
            fileLocation,
            request.Encrypted,
            encryptionKey: string.Empty,
            encryptionIV: string.Empty,
            DateTimeOffset.UtcNow);

        if (errorOrFileEntry.HasError)
        {
            return HandleFailure(errorOrFileEntry.Errors);
        }

        var file = errorOrFileEntry.Value;

        using var stream = request.FromFile.OpenReadStream();
        var createResult = await _fileManager.CreateAsync(file, stream);

        if (createResult.IsFailure)
        {
            return HandleFailure(createResult.Error);
        }

        _fileRepository.Add(file);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Ok(new FileCreatedResponse
        {
            Id = file.Id.Value,
        });
    }
}