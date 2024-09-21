using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.Max;

public static class MaxExtensions
{
    public static IServiceCollection AddMaxRestClient(
        this IServiceCollection services,
        string baseAddress)
    {
        services.AddTransient<RequestDebuggingMessageHandler>();
        services.AddTransient<MaxSignatureHandler>();

        services
            .AddHttpClient<IMaxRestClient, MaxRestClient>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Clear();
            })
            .AddHttpMessageHandler<RequestDebuggingMessageHandler>();

        return services;
    }
}