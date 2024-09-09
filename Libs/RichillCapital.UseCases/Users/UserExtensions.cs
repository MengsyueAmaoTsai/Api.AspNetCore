using RichillCapital.Domain;

namespace RichillCapital.UseCases.Users;

internal static class UserExtensions
{
    internal static UserDto ToDto(this User user) =>
        new()
        {
            Id = user.Id.Value,
            Name = user.Name,
            Email = user.Email.Value,
            EmailConfirmed = user.EmailConfirmed,
            CreatedTimeUtc = user.CreatedTimeUtc,
        };
}