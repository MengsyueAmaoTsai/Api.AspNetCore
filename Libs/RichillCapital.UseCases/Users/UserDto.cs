using RichillCapital.Domain.Users;

namespace RichillCapital.UseCases.Users;

public sealed record UserDto
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required string Email { get; init; }

    public required bool EmailConfirmed { get; init; }

    public required bool LockoutEnabled { get; init; }

    public required int AccessFailedCount { get; init; }

    public DateTimeOffset CreatedAt { get; init; }
}

internal static class UserExtensions
{
    internal static UserDto ToDto(this User user) =>
        new()
        {
            Id = user.Id.Value,
            Name = user.Name.Value,
            Email = user.Email.Value,
            EmailConfirmed = user.EmailConfirmed,
            LockoutEnabled = user.LockoutEnabled,
            AccessFailedCount = user.AccessFailedCount,
            CreatedAt = user.CreatedAt,
        };
}