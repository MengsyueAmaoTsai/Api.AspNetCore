using RichillCapital.UseCases.Files;

namespace RichillCapital.Contracts.Files;

public record FileEntryResponse
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required long Size { get; set; }

    public required DateTimeOffset UploadedTime { get; set; }

    public required string FileName { get; set; }

    public required string FileLocation { get; set; }

    public required bool Encrypted { get; set; }
}

public static class FileEntryResponseMapping
{
    public static FileEntryResponse ToResponse(this FileEntryDto dto) =>
        new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Size = dto.Size,
            UploadedTime = dto.UploadedTime,
            FileName = dto.FileName,
            FileLocation = dto.FileLocation,
            Encrypted = dto.Encrypted
        };

    public static IEnumerable<FileEntryResponse> ToResponse(this IEnumerable<FileEntryDto> files) =>
        files.Select(ToResponse);
}
