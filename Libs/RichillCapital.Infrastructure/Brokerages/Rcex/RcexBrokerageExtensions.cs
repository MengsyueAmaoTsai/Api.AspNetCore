using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.Infrastructure.Brokerages.Rcex;

public static class RcexBrokerageExtensions
{
    public static IServiceCollection AddRichillCapitalBrokerage(this IServiceCollection services)
    {
        return services;
    }
}