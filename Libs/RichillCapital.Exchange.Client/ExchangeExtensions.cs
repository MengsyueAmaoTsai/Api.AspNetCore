using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Http.Client.MessageHandlers;

namespace RichillCapital.Exchange.Client;

public static class ExchangeExtensions
{
    public static IServiceCollection AddExchangeRestClient(
        this IServiceCollection services,
        string baseAddress)
    {
        services.AddDefaultRequestDebuggingMessageHandler();

        services
            .AddHttpClient<IExchangeRestClient, ExchangeRestClient>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Clear();
            })
            .AddDefaultRequestDebuggingMessageHandler();

        return services;
    }
}