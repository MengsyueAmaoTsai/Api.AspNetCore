using Microsoft.Extensions.DependencyInjection;

using RichillCapital.UseCases.Common;

namespace RichillCapital.Identity;

public static class IdentityExtensions
{
    public static IServiceCollection AddApiIdentity(this IServiceCollection services)
    {
        // Current user context
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentApiUser>();

        return services;
    }
}