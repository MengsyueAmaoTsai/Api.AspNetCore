using RichillCapital.UseCases.Users;

namespace RichillCapital.Contracts.Users;

public sealed record UserDetailsResponse : UserResponse
{
}

public static class UserDetailsResponseMapping
{
    public static UserDetailsResponse ToDetailsResponse(this UserDto user) =>
        new()
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
        };
}