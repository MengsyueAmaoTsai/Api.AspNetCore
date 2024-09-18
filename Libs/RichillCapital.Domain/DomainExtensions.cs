using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.Domain;

public static class DomainExtensions
{
    public static IServiceCollection AddOrderPlacementEvaluator(
        this IServiceCollection services)
    {
        services.AddScoped<IMatchingEngine, FakeMatchingEngine>();
        return services;
    }
}