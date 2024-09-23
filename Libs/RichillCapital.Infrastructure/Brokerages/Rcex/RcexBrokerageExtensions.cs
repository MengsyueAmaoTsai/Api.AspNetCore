using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using RichillCapital.Exchange.Client;

namespace RichillCapital.Infrastructure.Brokerages.Rcex;

public static class RcexBrokerageExtensions
{
    public static IServiceCollection AddRichillCapitalBrokerage(this IServiceCollection services)
    {
        using var scope = services.BuildServiceProvider().CreateScope();

        var logger = scope.ServiceProvider.GetRequiredService<ILogger<RcexBrokerage>>();
        var options = scope.ServiceProvider.GetRequiredService<IOptions<BrokerageOptions>>().Value;

        var profile = options.Profiles.First(p => p.Provider == "RichillCapital");
        var baseAddress = profile.Arguments["BaseAddress"] as string ?? string.Empty;
        services.AddExchangeRestClient(baseAddress);

        logger.LogInformation("RichillCapital brokerage added. With arguments: {Arguments}", profile.Arguments);
        return services;
    }
}