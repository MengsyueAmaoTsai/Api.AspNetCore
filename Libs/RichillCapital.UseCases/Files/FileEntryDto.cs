using RichillCapital.Domain;

namespace RichillCapital.UseCases.Files;

public sealed record FileEntryDto
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public required long Size { get; init; }

    public required DateTimeOffset UploadedTime { get; init; }

    public required string FileName { get; init; }

    public required string FileLocation { get; init; }

    public required bool Encrypted { get; init; }
}

internal static class FileEntryExtensions
{
    internal static FileEntryDto ToDto(this FileEntry file) =>
        new()
        {
            Id = file.Id.Value,
            Name = file.Name,
            Description = file.Description,
            Size = file.Size,
            UploadedTime = file.UploadedTime,
            FileName = file.FileName,
            FileLocation = file.Location,
            Encrypted = file.Encrypted,
        };
}