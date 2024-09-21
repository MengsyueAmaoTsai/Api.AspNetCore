using FluentValidation;
using FluentValidation.Validators;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

using RichillCapital.Domain.Brokerages;
using RichillCapital.Extensions.Options;
using RichillCapital.Infrastructure.Brokerages.Rcex;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Brokerages;

public static class BrokerageExtensions
{
    public static IServiceCollection AddBrokerages(this IServiceCollection services)
    {
        services.AddBrokerageOptions();

        services.AddBrokerageFactory();
        services.AddBrokerageManager();

        services.AddRichillCapitalBrokerage();
        services.AddBinanceBrokerage();

        using var scope = services.BuildServiceProvider().CreateScope();

        var brokerageOptions = scope.ServiceProvider
            .GetRequiredService<IOptions<BrokerageOptions>>()
            .Value;

        var factory = scope.ServiceProvider.GetRequiredService<BrokerageFactory>();

        var brokerages = new BrokerageCollection();

        foreach (var profile in brokerageOptions.Profiles)
        {
            if (profile.Enabled)
            {
                factory
                    .CreateBrokerage(profile.Provider, profile.Name)
                    .ThrowIfFailure()
                    .Then(brokerages.Add)
                    .ThrowIfFailure();
            }
        }

        services.AddSingleton<IBrokerageCollection>(brokerages);

        return services;
    }

    private static IServiceCollection AddBrokerageOptions(
        this IServiceCollection services,
        string sectionKey = BrokerageOptions.SectionKey)
    {
        services.AddValidatorsFromAssembly(typeof(BrokerageExtensions).Assembly, includeInternalTypes: true);
        services.AddOptionsWithFluentValidation<BrokerageOptions>(sectionKey);
        return services;
    }

    private static IServiceCollection AddBrokerageFactory(this IServiceCollection services) =>
        services.AddSingleton<BrokerageFactory>();

    private static IServiceCollection AddBrokerageManager(this IServiceCollection services) =>
        services.AddSingleton<IBrokerageManager, BrokerageManager>();
}
