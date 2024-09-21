using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.Binance;

public static class BinanceExtensions
{
    public static IServiceCollection AddBinanceRestClient(
        this IServiceCollection services,
        string baseAddress)
    {
        services.AddTransient<BinanceSignatureHandler>();
        services.AddTransient<RequestDebuggingMessageHandler>();

        services
            .AddHttpClient<IBinanceRestClient, BinanceRestClient>(client =>
            {
                client.BaseAddress = new Uri("https://fapi.binance.com");
                client.DefaultRequestHeaders.Clear();
            })
            .AddHttpMessageHandler<RequestDebuggingMessageHandler>();

        return services;
    }
}