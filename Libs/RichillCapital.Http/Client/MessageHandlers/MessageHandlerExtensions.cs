using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.Http.Client.MessageHandlers;

public static class DefaultRequestDebuggingMessageHandlerExtensions
{
    public static IServiceCollection AddDefaultRequestDebuggingMessageHandler(
        this IServiceCollection services)
    {
        services.AddTransient<DefaultRequestDebuggingMessageHandler>();

        return services;
    }

    public static IHttpClientBuilder AddDefaultRequestDebuggingMessageHandler(
        this IHttpClientBuilder builder) =>
        builder.AddHttpMessageHandler<DefaultRequestDebuggingMessageHandler>();
}