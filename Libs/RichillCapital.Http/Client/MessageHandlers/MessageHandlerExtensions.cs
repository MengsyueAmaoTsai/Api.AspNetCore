using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RichillCapital.Http.Client.MessageHandlers;

public static class DefaultRequestDebuggingMessageHandlerExtensions
{
    public static IServiceCollection AddDefaultRequestDebuggingMessageHandler(
        this IServiceCollection services)
    {
        services.TryAddTransient<DefaultRequestDebuggingMessageHandler>();

        return services;
    }

    public static IHttpClientBuilder AddDefaultRequestDebuggingMessageHandler(
        this IHttpClientBuilder builder) =>
        builder.AddHttpMessageHandler<DefaultRequestDebuggingMessageHandler>();
}