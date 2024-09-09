using RichillCapital.UseCases.Users;

namespace RichillCapital.Contracts.Users;

public sealed record UserResponse
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required bool EmailConfirmed { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}

public static class UserResponseMapping
{
    public static UserResponse ToResponse(this UserDto dto) =>
        new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Email = dto.Email,
            EmailConfirmed = dto.EmailConfirmed,
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };
}