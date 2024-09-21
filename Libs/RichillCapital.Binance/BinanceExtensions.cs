using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.Binance;

public static class BinanceExtensions
{
    private const string BaseAddress = "https://fapi.binance.com";
    private const string BaseAddressTestnet = "https://testnet.binancefuture.com";

    public static IServiceCollection AddBinanceRestClient(
        this IServiceCollection services,
        string baseAddress)
    {
        services.AddTransient<BinanceSignatureHandler>();
        services.AddTransient<RequestDebuggingMessageHandler>();

        services
            .AddHttpClient<IBinanceRestClient, BinanceRestClient>(client =>
            {
                client.BaseAddress = new Uri(BaseAddressTestnet);
                client.DefaultRequestHeaders.Clear();
            })
            .AddHttpMessageHandler<RequestDebuggingMessageHandler>();

        return services;
    }
}