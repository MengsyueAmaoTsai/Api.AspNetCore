using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.Binance;

public static class BinanceExtensions
{
    public static IServiceCollection AddBinanceRestService(
        this IServiceCollection services)
    {
        services.AddHttpClient<BinanceRestService>(client =>
        {
            client.BaseAddress = new Uri("https://api.binance.com");
            client.DefaultRequestHeaders.Clear();
        });

        return services;
    }
}