using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.Binance;

public static class BinanceExtensions
{
    public static IServiceCollection AddBinanceSpotRestClient(
        this IServiceCollection services)
    {
        services.AddHttpClient<IBinanceSpotRestClient, BinanceSpotRestClient>(client =>
        {

        });

        return services;
    }
}