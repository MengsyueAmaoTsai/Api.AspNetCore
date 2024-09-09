namespace RichillCapital.UseCases.Users;

public sealed record UserDto
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required bool EmailConfirmed { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}
