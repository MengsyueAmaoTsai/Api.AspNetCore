using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Http.Client.MessageHandlers;

namespace RichillCapital.Max;

public static class MaxExtensions
{
    public static IServiceCollection AddMaxRestClient(
        this IServiceCollection services,
        string baseAddress)
    {
        services.AddDefaultRequestDebuggingMessageHandler();
        services.AddTransient<MaxSignatureHandler>();
        services.AddScoped<MaxResponseHandler>();

        services
            .AddHttpClient<IMaxRestClient, MaxRestClient>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Clear();
            })
            .AddDefaultRequestDebuggingMessageHandler();

        return services;
    }
}