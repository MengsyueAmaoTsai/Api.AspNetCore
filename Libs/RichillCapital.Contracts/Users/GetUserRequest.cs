using Microsoft.AspNetCore.Mvc;

namespace RichillCapital.Contracts.Users;

public sealed record GetUserRequest
{
    [FromRoute(Name = "userId")]
    public required string UserId { get; init; }
}