using Microsoft.AspNetCore.Mvc;

using RichillCapital.UseCases.Users.Get;

namespace RichillCapital.Contracts.Users;

public sealed record GetUserByIdRequest
{
    [FromRoute(Name = "userId")]
    public required string UserId { get; init; }
}

public static class GetUserByIdRequestMapping
{
    public static GetUserByIdQuery ToQuery(this GetUserByIdRequest request) =>
        new()
        {
            UserId = request.UserId,
        };
}