using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Exchange.Client;

namespace RichillCapital.Infrastructure.Brokerages.Rcex;

public static class RcexBrokerageExtensions
{
    public static IServiceCollection AddRichillCapitalBrokerage(this IServiceCollection services)
    {
        services.AddExchangeRestClient();

        return services;
    }
}