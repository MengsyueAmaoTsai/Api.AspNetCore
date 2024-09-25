using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain;

public static class DomainExtensions
{
    public static IServiceCollection AddDomainServices(
        this IServiceCollection services)
    {
        services.AddCore();
        services.AddSignals();
        services.AddServerSideCopyTrading();

        return services;
    }

    private static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IMatchingEngine, FakeMatchingEngine>();

        return services;
    }

    private static IServiceCollection AddSignals(this IServiceCollection services)
    {
        services.AddScoped<ISignalManager, SignalManager>();
        services.AddScoped<ISignalSourceManager, SignalSourceManager>();

        return services;
    }

    private static IServiceCollection AddServerSideCopyTrading(this IServiceCollection services)
    {
        services.AddScoped<ICopyTradingService, CopyTradingService>();

        return services;
    }
}