using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class FileEntry : Entity<FileEntryId>
{
    private FileEntry(
        FileEntryId id,
        string name,
        string description,
        long size,
        DateTimeOffset uploadedTime,
        string fileName,
        string location,
        bool encrypted,
        string encryptionKey,
        string encryptionIV) : base(id)
    {
        Name = name;
        Description = description;
        Size = size;
        UploadedTime = uploadedTime;
        FileName = fileName;
        Location = location;
        Encrypted = encrypted;
        EncryptionKey = encryptionKey;
        EncryptionIV = encryptionIV;
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public long Size { get; private set; }

    public DateTimeOffset UploadedTime { get; private set; }

    public string FileName { get; private set; }

    public string Location { get; private set; }

    public bool Encrypted { get; private set; }

    public string EncryptionKey { get; private set; }

    public string EncryptionIV { get; private set; }

    public static ErrorOr<FileEntry> Create(
        FileEntryId id,
        string name,
        string description,
        long size,
        DateTimeOffset uploadedTime,
        string fileName,
        string location,
        bool encrypted,
        string encryptionKey,
        string encryptionIV)
    {
        var file = new FileEntry(
            FileEntryId.NewFileEntryId(),
            name,
            description,
            size,
            uploadedTime,
            fileName,
            location,
            encrypted,
            encryptionKey,
            encryptionIV);

        return file.ToErrorOr();
    }
}
