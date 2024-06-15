using System.Security.Claims;

using RichillCapital.Domain;

namespace RichillCapital.Identity;

internal static class ClaimsPrincipalExtensions
{
    public static UserId GetId(this ClaimsPrincipal user) =>
        UserId.From(user.Claims.First(claim => claim.Type == "sub").Value).Value;
}