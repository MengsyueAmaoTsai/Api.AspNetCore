using RichillCapital.UseCases.Users;

namespace RichillCapital.Contracts.Users;

public record UserResponse
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required bool EmailConfirmed { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}

public sealed record UserDetailsResponse : UserResponse
{
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

    public static UserDetailsResponse ToDetailsResponse(this UserDto dto) =>
        new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Email = dto.Email,
            EmailConfirmed = dto.EmailConfirmed,
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };
}
