using RichillCapital.UseCases.Common;
using RichillCapital.UseCases.Users;

namespace RichillCapital.Contracts.Users;

public record UserResponse
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required string Email { get; init; }

    public DateTimeOffset CreatedAt { get; init; }
}

public static class UserResponseMapping
{
    public static UserResponse ToResponse(this UserDto dto) =>
        new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Email = dto.Email,
            CreatedAt = dto.CreatedAt
        };

    public static Paged<UserResponse> ToPagedResponse(this PagedDto<UserDto> dto) =>
        new()
        {
            Items = dto.Items.Select(ToResponse),
            TotalCount = dto.TotalCount,
            Page = dto.Page,
            PageSize = dto.PageSize,
        };
}