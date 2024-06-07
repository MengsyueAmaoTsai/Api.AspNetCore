namespace RichillCapital.Domain.Storage;

public interface IFileStorageManager
{
    Task CreateAsync(FileEntry fileEntry, Stream stream, CancellationToken cancellationToken = default);
}