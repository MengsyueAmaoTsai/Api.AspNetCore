using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using RichillCapital.UseCases.Common;

namespace RichillCapital.Identity;

internal sealed class CurrentApiUser(
    IHttpContextAccessor _httpContextAccessor) :
    ICurrentUser
{
    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ??
            throw new ApplicationException("User context is unavailable");

    public string Id => _httpContextAccessor.HttpContext!.User.Claims
        .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ??
            throw new ApplicationException("User context is unavailable");
}
