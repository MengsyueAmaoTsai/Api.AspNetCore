using RichillCapital.UseCases.Files;

namespace RichillCapital.Contracts.Files;

public sealed record FileEntryDetailsResponse : FileEntryResponse
{
}

public static class FileEntryDetailsResponseMapping
{
    public static FileEntryDetailsResponse ToDetailsResponse(this FileEntryDto dto) =>
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
}

