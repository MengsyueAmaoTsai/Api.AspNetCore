namespace RichillCapital.Domain.Common.Storage;

public interface IFileStorageManager
{
    Task CreateAsync(FileEntry fileEntry, Stream stream, CancellationToken cancellationToken = default);

    Task<byte[]> ReadAsync(FileEntry fileEntry, CancellationToken cancellationToken = default);

    Task DeleteAsync(FileEntry fileEntry, CancellationToken cancellationToken = default);

    Task ArchiveAsync(FileEntry fileEntry, CancellationToken cancellationToken = default);

    Task UnArchiveAsync(FileEntry fileEntry, CancellationToken cancellationToken = default);
}