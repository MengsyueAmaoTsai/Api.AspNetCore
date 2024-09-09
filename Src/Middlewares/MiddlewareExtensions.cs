namespace RichillCapital.Api.Middlewares;

internal static class MiddlewareExtensions
{
    internal static IServiceCollection AddMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<RequestDebuggingMiddleware>();
        services.AddScoped<SignalDebuggingMiddleware>();

        return services;
    }

    internal static IApplicationBuilder UseRequestDebuggingMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestDebuggingMiddleware>();

        return app;
    }

    internal static IApplicationBuilder UseSignalDebuggingMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<SignalDebuggingMiddleware>();

        return app;
    }
}
