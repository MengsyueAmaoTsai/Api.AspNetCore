namespace RichillCapital.Domain.Storage;

public interface IFileStorageManager
{
    Task CreateAsync(FileEntry fileEntry, Stream stream, CancellationToken cancellationToken = default);

    Task<byte[]> ReadAsync(FileEntry fileEntry, CancellationToken cancellationToken = default);
}