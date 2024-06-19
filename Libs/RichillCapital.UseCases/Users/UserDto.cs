using RichillCapital.Domain;

namespace RichillCapital.UseCases.Users;

public sealed record UserDto
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string PhoneNumber { get; init; }
    public required bool LockoutEnabled { get; init; }
    public required bool TwoFactorEnabled { get; init; }
    public required bool EmailConfirmed { get; init; }
    public required bool PhoneNumberConfirmed { get; init; }
    public required int AccessFailedCount { get; init; }
    public required DateTimeOffset LockoutEnd { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
}

internal static class UserExtensions
{
    internal static UserDto ToDto(this User user) =>
        new()
        {
            Id = user.Id.Value,
            Email = user.Email.Value,
            Name = user.Name.Value,
            PhoneNumber = user.PhoneNumber.Value,
            LockoutEnabled = user.LockoutEnabled,
            TwoFactorEnabled = user.TwoFactorEnabled,
            EmailConfirmed = user.EmailConfirmed,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            AccessFailedCount = user.AccessFailedCount,
            LockoutEnd = user.LockoutEnd,
            CreatedAt = user.CreatedAt,
        };
}