using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Max;

namespace RichillCapital.Infrastructure.Brokerages.Max;

public static class MaxBrokerageExtensions
{
    public static IServiceCollection AddMaxBrokerage(this IServiceCollection services)
    {
        services.AddMaxRestClient("https://max-api.maicoin.com");

        return services;
    }
}