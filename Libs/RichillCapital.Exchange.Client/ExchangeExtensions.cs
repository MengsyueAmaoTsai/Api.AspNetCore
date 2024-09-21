using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.Exchange.Client;

public static class ExchangeExtensions
{
    public static IServiceCollection AddExchangeRestClient(this IServiceCollection services)
    {
        services
            .AddHttpClient<IExchangeRestClient, ExchangeRestClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:10000");
                client.DefaultRequestHeaders.Clear();
            });

        return services;
    }
}