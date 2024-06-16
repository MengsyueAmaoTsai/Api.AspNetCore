using FluentValidation;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using RichillCapital.Extensions.Options;
using RichillCapital.UseCases.Common;

namespace RichillCapital.Identity;

public static class IdentityExtensions
{
    public static IServiceCollection AddApiIdentity(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(
            typeof(IdentityExtensions).Assembly,
            includeInternalTypes: true);

        services.AddOptionsWithFluentValidation<IdentityOptions>(IdentityOptions.SectionKey);

        using var scope = services
            .BuildServiceProvider()
            .CreateScope();

        var identityOptions = scope.ServiceProvider
            .GetRequiredService<IOptions<IdentityOptions>>().Value;

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = RichillCapitalAuthenticationSchemes.JwtDefault;
        })
        .AddJwtBearer(RichillCapitalAuthenticationSchemes.JwtDefault, options =>
        {
            options.Authority = identityOptions.Authority;
            options.Audience = identityOptions.Audience;
            options.RequireHttpsMetadata = identityOptions.RequireHttpsMetadata;
        });

        // Current user context
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentApiUser>();

        return services;
    }
}