using Microsoft.AspNetCore.Http;

namespace RichillCapital.Contracts.Files;

public sealed record CreateFileRequest
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required IFormFile FromFile { get; init; }
    public required bool Encrypted { get; init; }
}
