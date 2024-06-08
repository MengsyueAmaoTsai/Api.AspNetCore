using RichillCapital.UseCases.Common;
using RichillCapital.UseCases.Users;

namespace RichillCapital.Contracts.Users;

public record UserResponse
{
    public required string Id { get; init; }

    public required string Email { get; init; }

    public required string Name { get; init; }
}

public static class UserResponseMapping
{
    public static UserResponse ToResponse(this UserDto user) =>
        new()
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
        };

    public static Paged<UserResponse> ToResponse(this PagedDto<UserDto> dto) =>
        new()
        {
            Items = dto.Items
                .Select(ToResponse),
        };
}
