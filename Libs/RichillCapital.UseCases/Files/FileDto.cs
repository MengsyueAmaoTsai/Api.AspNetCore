namespace RichillCapital.UseCases.Files;

public sealed record FileDto
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required long Size { get; init; }
    public required string FileName { get; init; }
    public required string Location { get; init; }
    public required bool Encrypted { get; init; }
    public required string EncryptionKey { get; init; }
    public required string EncryptionIV { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}