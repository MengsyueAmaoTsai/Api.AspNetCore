using FluentValidation;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using RichillCapital.Domain.DataFeeds;
using RichillCapital.Extensions.Options;
using RichillCapital.Infrastructure.DataFeeds.Max;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.DataFeeds;

public static class DataFeedExtensions
{
    public static IServiceCollection AddDataFeeds(this IServiceCollection services)
    {
        services.AddDataFeedOptions();

        services.AddDataFeedFactory();
        services.AddDataFeedManager();

        // services.AddRichillCapitalDataFeed();
        // services.AddBinanceDataFeed();
        services.AddMaxDataFeed();

        using var scope = services.BuildServiceProvider().CreateScope();

        var DataFeedOptions = scope.ServiceProvider
            .GetRequiredService<IOptions<DataFeedOptions>>()
            .Value;

        var factory = scope.ServiceProvider.GetRequiredService<DataFeedFactory>();

        var DataFeeds = new DataFeedCollection();

        foreach (var profile in DataFeedOptions.Profiles)
        {
            if (profile.Enabled)
            {
                factory
                    .CreateDataFeed(profile)
                    .ThrowIfFailure()
                    .Then(DataFeeds.Add)
                    .ThrowIfFailure();
            }
        }

        services.AddSingleton<IDataFeedCollection>(DataFeeds);

        return services;
    }

    private static IServiceCollection AddDataFeedOptions(
        this IServiceCollection services,
        string sectionKey = DataFeedOptions.SectionKey)
    {
        services.AddValidatorsFromAssembly(typeof(DataFeedExtensions).Assembly, includeInternalTypes: true);
        services.AddOptionsWithFluentValidation<DataFeedOptions>(sectionKey);
        return services;
    }

    private static IServiceCollection AddDataFeedFactory(this IServiceCollection services) =>
        services.AddSingleton<DataFeedFactory>();

    private static IServiceCollection AddDataFeedManager(this IServiceCollection services) =>
        services.AddSingleton<IDataFeedManager, DataFeedManager>();
}
