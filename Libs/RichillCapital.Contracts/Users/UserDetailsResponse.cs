using RichillCapital.Contracts.Accounts;
using RichillCapital.UseCases.Users;

namespace RichillCapital.Contracts.Users;

public sealed record UserDetailsResponse : UserResponse
{
    public required bool EmailConfirmed { get; init; }

    public required bool LockoutEnabled { get; init; }

    public required int AccessFailedCount { get; init; }

    public required IEnumerable<AccountResponse> Accounts { get; init; }
}

public static class UserDetailsResponseMapping
{
    public static UserDetailsResponse ToDetailsResponse(this UserDto dto) =>
        new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Email = dto.Email,
            EmailConfirmed = dto.EmailConfirmed,
            LockoutEnabled = dto.LockoutEnabled,
            AccessFailedCount = dto.AccessFailedCount,
            CreatedAt = dto.CreatedAt,
            Accounts = dto.Accounts.Select(account => account.ToResponse()),
        };
}