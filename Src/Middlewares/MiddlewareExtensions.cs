namespace RichillCapital.Api.Middlewares;

internal static class MiddlewareExtensions
{
    internal static IServiceCollection AddMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<DebuggingRequestMiddleware>();

        return services;
    }

    internal static IApplicationBuilder UseDebuggingRequestMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<DebuggingRequestMiddleware>();

        return app;
    }
}
