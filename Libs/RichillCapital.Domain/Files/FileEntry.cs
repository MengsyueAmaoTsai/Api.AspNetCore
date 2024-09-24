using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Files;

public sealed class FileEntry : Entity<FileEntryId>
{
    private FileEntry(
        FileEntryId id,
        string name,
        string description,
        long size,
        string fileName,
        string location,
        bool encrypted,
        string encryptionKey,
        string encryptionIV,
        DateTimeOffset createdTimeUtc)
        : base(id)
    {
        Name = name;
        Description = description;
        Size = size;
        FileName = fileName;
        Location = location;
        Encrypted = encrypted;
        EncryptionKey = encryptionKey;
        EncryptionIV = encryptionIV;
        CreatedTimeUtc = createdTimeUtc;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public long Size { get; private set; }
    public string FileName { get; private set; }
    public string Location { get; private set; }
    public bool Encrypted { get; private set; }
    public string EncryptionKey { get; private set; }
    public string EncryptionIV { get; private set; }
    public DateTimeOffset CreatedTimeUtc { get; private set; }

    public static ErrorOr<FileEntry> Create(
        FileEntryId id,
        string name,
        string description,
        long size,
        string fileName,
        string location,
        bool encrypted,
        string encryptionKey,
        string encryptionIV,
        DateTimeOffset createdTimeUtc)
    {
        var file = new FileEntry(
            id,
            name: name,
            description: description,
            size: size,
            fileName: fileName,
            location: location,
            encrypted: encrypted,
            encryptionKey: encryptionKey,
            encryptionIV: encryptionIV,
            createdTimeUtc: createdTimeUtc);

        return ErrorOr<FileEntry>.With(file);
    }
}
