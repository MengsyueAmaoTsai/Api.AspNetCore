using RichillCapital.Domain;

namespace RichillCapital.UseCases.Users;

public sealed record UserDto
{
    public required string Id { get; init; }
}

internal static class UserExtensions
{
    internal static UserDto ToDto(this User user) =>
        new()
        {
            Id = user.Id.Value,
        };
}