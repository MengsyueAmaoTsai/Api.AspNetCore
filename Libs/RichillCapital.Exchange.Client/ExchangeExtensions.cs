using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Http;

namespace RichillCapital.Exchange.Client;

public static class ExchangeExtensions
{
    public static IServiceCollection AddExchangeRestClient(this IServiceCollection services)
    {
        services.AddDefaultRequestDebuggingMessageHandler();

        services
            .AddHttpClient<IExchangeRestClient, ExchangeRestClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:10000");
                client.DefaultRequestHeaders.Clear();
            })
            .AddDefaultRequestDebuggingMessageHandler();

        return services;
    }
}