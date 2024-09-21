using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Binance;

public static class BinanceBrokerageExtensions
{
    public static IServiceCollection AddBinanceBrokerage(this IServiceCollection services)
    {
        services.AddBinanceSpotRestClient();

        return services;
    }
}