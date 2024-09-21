using Microsoft.Extensions.DependencyInjection;

public static class BinanceBrokerageExtensions
{
    public static IServiceCollection AddBinanceBrokerage(this IServiceCollection services)
    {
        // services.AddBinanceSignatureService();
        // services.AddBinanceUsdMarginedRestClient("https://fapi.binance.com");

        return services;
    }
}