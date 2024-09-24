using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain;

public static class DomainExtensions
{
    public static IServiceCollection AddOrderPlacementEvaluator(
        this IServiceCollection services)
    {
        services.AddScoped<IMatchingEngine, FakeMatchingEngine>();
        services.AddScoped<IPaperTradingService, PaperTradingService>();

        services.AddScoped<ISignalManager, SignalManager>();
        services.AddScoped<ISignalSourceManager, SignalSourceManager>();

        services.AddScoped<ICopyTradingService, CopyTradingService>();

        return services;
    }
}