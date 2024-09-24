using RichillCapital.Domain.Files;

namespace RichillCapital.UseCases.Files;

internal static class FileExtensions
{
    internal static FileDto ToDto(this FileEntry file) =>
        new()
        {
            Id = file.Id.Value,
            Name = file.Name,
            Description = file.Description,
            Size = file.Size,
            FileName = file.FileName,
            Location = file.Location,
            Encrypted = file.Encrypted,
            EncryptionKey = file.EncryptionKey,
            EncryptionIV = file.EncryptionIV,
            CreatedTimeUtc = file.CreatedTimeUtc,
        };
}