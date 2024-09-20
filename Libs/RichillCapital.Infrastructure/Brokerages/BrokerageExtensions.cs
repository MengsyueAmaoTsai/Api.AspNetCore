using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Domain.Brokerages;
using RichillCapital.Infrastructure.Brokerages.Rcex;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Brokerages;

public static class BrokerageExtensions
{
    public static IServiceCollection AddBrokerages(this IServiceCollection services)
    {
        services.AddSingleton<BrokerageFactory>();
        services.AddSingleton<IBrokerageManager, BrokerageManager>();

        services.AddRichillCapitalBrokerage();
        services.AddBinanceBrokerage();

        using var scope = services.BuildServiceProvider().CreateScope();

        List<(string Provider, string Name, bool StartOnBoot, bool Enabled)> profiles = [
            ("RichillCapital", "RichillCapital 1", true, true),
            ("RichillCapital", "RichillCapital 2", true, true),
            ("Binance", "Binance", true, true),
        ];

        var factory = scope.ServiceProvider.GetRequiredService<BrokerageFactory>();

        var brokerages = new BrokerageCollection();

        foreach (var (provider, name, startOnBoot, enabled) in profiles)
        {
            if (enabled)
            {
                factory
                    .CreateBrokerage(provider, name)
                    .ThrowIfFailure()
                    .Then(brokerages.Add)
                    .ThrowIfFailure();
            }
        }

        services.AddSingleton<IBrokerageCollection>(brokerages);

        return services;
    }
}
