namespace RichillCapital.Contracts.Users;

public sealed record CreateUserResponse
{
    public required string Id { get; init; }
}