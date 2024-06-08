using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Common.Storage;

namespace RichillCapital.Storage;

internal sealed class LocalFileStorageManager(
    ILogger<LocalFileStorageManager> _logger) :
    IFileStorageManager
{
    private const string ManagingDirectory = @"R:/";
    public Task ArchiveAsync(
        FileEntry fileEntry,
        CancellationToken cancellationToken = default)
    {
        _logger.LogWarning("Archive operation is not supported for local file storage manager.");
        return Task.CompletedTask;
    }

    public async Task CreateAsync(
        FileEntry fileEntry,
        Stream stream,
        CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(ManagingDirectory, fileEntry.Location);
        var directoryName = Path.GetDirectoryName(filePath);

        if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
        {
            Directory.CreateDirectory(directoryName);
        }

        using var fileStream = File.Create(filePath);

        await stream.CopyToAsync(stream, cancellationToken);
    }

    public async Task DeleteAsync(
        FileEntry fileEntry,
        CancellationToken cancellationToken = default) =>
        await Task.Run(
            () =>
            {
                var path = Path.Combine(ManagingDirectory, fileEntry.Location);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            },
            cancellationToken);

    public Task<byte[]> ReadAsync(
        FileEntry fileEntry,
        CancellationToken cancellationToken = default) =>
        File.ReadAllBytesAsync(
            Path.Combine(ManagingDirectory, fileEntry.Location),
            cancellationToken);

    public Task UnArchiveAsync(
        FileEntry fileEntry,
        CancellationToken cancellationToken = default)
    {
        _logger.LogWarning("Unarchive operation is not supported for local file storage manager.");
        return Task.CompletedTask;
    }
}

