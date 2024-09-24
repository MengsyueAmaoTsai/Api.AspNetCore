namespace RichillCapital.Infrastructure.Storage.Local;

public sealed record LocalStorageOptions
{
    public required string Path { get; init; }
}
