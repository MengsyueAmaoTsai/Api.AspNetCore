using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Http.Client.MessageHandlers;

namespace RichillCapital.Binance;

public static class BinanceExtensions
{
    private const string BaseAddress = "https://fapi.binance.com";
    private const string BaseAddressTestnet = "https://testnet.binancefuture.com";

    public static IServiceCollection AddBinanceRestClient(
        this IServiceCollection services,
        string baseAddress)
    {
        services.AddDefaultRequestDebuggingMessageHandler();
        services.AddTransient<BinanceSignatureHandler>();

        services
            .AddHttpClient<IBinanceRestClient, BinanceRestClient>(client =>
            {
                client.BaseAddress = new Uri(BaseAddressTestnet);
                client.DefaultRequestHeaders.Clear();
            })
            .AddDefaultRequestDebuggingMessageHandler();

        return services;
    }
}