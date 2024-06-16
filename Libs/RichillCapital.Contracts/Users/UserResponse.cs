using RichillCapital.UseCases.Common;
using RichillCapital.UseCases.Users;

namespace RichillCapital.Contracts.Users;

public record UserResponse
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
}

public static class UserResponseMapping
{
    public static UserResponse ToResponse(this UserDto user) =>
        new()
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber,
            LockoutEnabled = user.LockoutEnabled,
            TwoFactorEnabled = user.TwoFactorEnabled,
            EmailConfirmed = user.EmailConfirmed,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            AccessFailedCount = user.AccessFailedCount,
            LockoutEnd = user.LockoutEnd,
        };

    public static Paged<UserResponse> ToResponse(this PagedDto<UserDto> dto) =>
        new()
        {
            Items = dto.Items
                .Select(ToResponse),
        };
}
