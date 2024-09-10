using FluentValidation;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using RichillCapital.Extensions.Options;

namespace RichillCapital.Infrastructure.Identity;

public static class IdentityExtensions
{
    public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(
            typeof(IdentityExtensions).Assembly,
            includeInternalTypes: true);

        services.AddOptionsWithFluentValidation<IdentityOptions>(IdentityOptions.SectionKey);

        using var scope = services.BuildServiceProvider().CreateScope();
        var identityOptions = scope.ServiceProvider.GetRequiredService<IOptions<IdentityOptions>>().Value;

        services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = CustomAuthenticationSchemes.JwtBearerDefault;
            })
            .AddJwtBearer(
                CustomAuthenticationSchemes.JwtBearerDefault,
                options =>
                {
                    options.Authority = identityOptions.Authority;
                    options.Audience = identityOptions.Audience;
                    options.RequireHttpsMetadata = identityOptions.RequireHttpsMetadata;
                });

        services.AddHttpContextAccessor();

        return services;
    }
}