namespace RichillCapital.UseCases.Files;

public sealed record FileDto
{
    public required string Name { get; init; }

    public required byte[] Content { get; init; }
}