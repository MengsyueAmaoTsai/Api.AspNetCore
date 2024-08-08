using RichillCapital.UseCases.Users;

namespace RichillCapital.Contracts.Users;

public sealed record UserDetailsResponse
{
    public required string Id { get; init; }
}

public static class UserDetailsResponseMapping
{
    public static UserDetailsResponse ToDetailsResponse(this UserDto user) =>
        new()
        {
            Id = user.Id,
        };
}