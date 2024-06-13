using Microsoft.Extensions.DependencyInjection;

using RichillCapital.UseCases.Common;

namespace RichillCapital.Identity;

public static class IdentityExtensions
{
    public static IServiceCollection AddApiIdentity(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUser, FakeUser>();

        return services;
    }
}