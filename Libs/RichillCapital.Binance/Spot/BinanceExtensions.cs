using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.Binance.Spot;

public static class BinanceExtensions
{
    private const string SpotBaseAddress = "https://api.binance.com";
    private const string SpotTestNetAddress = "https://testnet.binance.vision";

    public static IServiceCollection AddBinanceSpotRestClient(
        this IServiceCollection services)
    {
        services.AddHttpClient<IBinanceSpotRestClient, BinanceSpotRestClient>(client =>
        {
            client.BaseAddress = new Uri(SpotTestNetAddress);
            client.DefaultRequestHeaders.Clear();
        });

        return services;
    }
}