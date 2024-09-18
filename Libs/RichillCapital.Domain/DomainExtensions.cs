using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Domain;

public static class DomainExtensions
{
    public static IServiceCollection AddOrderPlacementEvaluator(
        this IServiceCollection services)
    {
        services.AddScoped<IOrderMatcher, FakeOrderMatcher>();
        services.AddScoped<IOrderBooks, InMemoryOrderBooks>();

        return services;
    }
}