using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Files;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Storage.Local;

internal sealed class LocalFileStorageManager(
    ILogger<LocalFileStorageManager> _logger,
    IOptions<StorageOptions> options) :
    IFileStorageManager
{
    private readonly LocalStorageOptions _options = options.Value.Local;

    public async Task<Result> CreateAsync(
        FileEntry fileEntry,
        Stream stream,
        CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(_options.Path, fileEntry.Location);
        var directory = Path.GetDirectoryName(filePath);

        if (string.IsNullOrEmpty(directory))
        {
            _logger.LogError("The directory for the file {FileId} is invalid.", fileEntry.Id);
            return Result.Failure(Error.Invalid($"The directory {directory} for the file {fileEntry.Id} cannot be null or empty."));
        }

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        using var fileStream = File.Create(filePath);

        await stream.CopyToAsync(fileStream, cancellationToken);

        _logger.LogInformation("[FileCreatedDomainEvent] - File {FileId} has been created at {FilePath}.", fileEntry.Id, filePath);

        return Result.Success;
    }

    public async Task<Result> DeleteAsync(FileEntry fileEntry, CancellationToken cancellationToken = default)
    {
        var path = Path.Combine(_options.Path, fileEntry.Location);

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        return await Task.FromResult(Result.Success);
    }

    public async Task<Result<byte[]>> ReadAsync(FileEntry fileEntry, CancellationToken cancellationToken = default)
    {
        var bytes = await File.ReadAllBytesAsync(Path.Combine(_options.Path, fileEntry.Location), cancellationToken);

        return Result<byte[]>.With(bytes);
    }
}
