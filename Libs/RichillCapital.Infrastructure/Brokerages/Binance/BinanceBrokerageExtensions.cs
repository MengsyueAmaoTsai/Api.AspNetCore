using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Binance;

namespace RichillCapital.Infrastructure.Brokerages.Binance;

public static class BinanceBrokerageExtensions
{
    public static IServiceCollection AddBinanceBrokerage(this IServiceCollection services)
    {
        services.AddBinanceRestClient("https://fapi.binance.com");

        return services;
    }
}