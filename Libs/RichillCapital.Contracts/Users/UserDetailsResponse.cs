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
            Name = user.Name,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            LockoutEnabled = user.LockoutEnabled,
            TwoFactorEnabled = user.TwoFactorEnabled,
            EmailConfirmed = user.EmailConfirmed,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            AccessFailedCount = user.AccessFailedCount,
            LockoutEnd = user.LockoutEnd,
        };
}