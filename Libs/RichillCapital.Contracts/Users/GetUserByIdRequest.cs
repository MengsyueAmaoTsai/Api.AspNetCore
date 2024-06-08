using Microsoft.AspNetCore.Mvc;

namespace RichillCapital.Contracts.Users;

public sealed record GetUserByIdRequest
{
    [FromRoute(Name = "userId")]
    public required string UserId { get; init; }
}

public static class GetUserByIdRequestMapping
{
}