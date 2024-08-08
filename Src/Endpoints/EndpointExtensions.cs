using Asp.Versioning;

using RichillCapital.Api.OpenApi;

namespace RichillCapital.Api.Endpoints;

internal static class EndpointExtensions
{
    internal static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        services
            .AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
            })
            .AddJsonOptions(options =>
            {
            });

        services.AddEndpointsApiExplorer();

        services.AddEndpointVersioning();

        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }

    internal static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapControllers();

        builder.MapGCInfoEndpoint();
        builder.MapThreadPoolInfoEndpoint();
        builder.MapProcessInfoEndpoint();

        return builder;
    }

    private static IServiceCollection AddEndpointVersioning(this IServiceCollection services)
    {
        services.ConfigureOptions<ConfigureSwaggerOptions>();

        services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(EndpointVersion.V1);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

        return services;
    }
}