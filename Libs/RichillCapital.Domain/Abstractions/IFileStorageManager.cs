using RichillCapital.Domain.Files;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Abstractions;

public interface IFileStorageManager
{
    Task<Result> CreateAsync(FileEntry fileEntry, Stream stream, CancellationToken cancellationToken = default);
}