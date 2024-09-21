using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Binance;

public static class BinanceBrokerageExtensions
{
    public static IServiceCollection AddBinanceBrokerage(this IServiceCollection services)
    {
        services.AddHttpClient<BinanceRestService>(client =>
        {
            client.BaseAddress = new Uri("https://api.binance.com");
            client.DefaultRequestHeaders.Clear();
        });

        return services;
    }
}