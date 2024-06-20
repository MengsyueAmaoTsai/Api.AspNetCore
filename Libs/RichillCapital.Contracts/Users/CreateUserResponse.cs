using RichillCapital.Domain.Users;

namespace RichillCapital.Contracts.Users;

public sealed record CreateUserResponse
{
    public required string Id { get; init; }
}

public static class  CreateUserResponseMapping
{
    public static CreateUserResponse ToResponse(this UserId userId) =>
        new()
        {
            Id = userId.Value
        };
}