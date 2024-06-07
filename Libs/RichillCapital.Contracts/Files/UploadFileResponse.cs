using RichillCapital.Domain;

namespace RichillCapital.Contracts.Files;

public sealed record UploadFileResponse
{
    public required Guid Id { get; init; }
}

public static class UploadFileResponseMapping
{
    public static UploadFileResponse ToResponse(this FileEntryId id) =>
        new()
        {
            Id = id.Value,
        };
}