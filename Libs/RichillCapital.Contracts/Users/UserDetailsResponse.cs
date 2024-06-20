using RichillCapital.UseCases.Users;

namespace RichillCapital.Contracts.Users;

public sealed record UserDetailsResponse : UserResponse
{
}

public static class UserDetailsResponseMapping
{
    public static UserDetailsResponse ToDetailsResponse(this UserDto dto) =>
        new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Email = dto.Email,
            CreatedAt = dto.CreatedAt
        };
}