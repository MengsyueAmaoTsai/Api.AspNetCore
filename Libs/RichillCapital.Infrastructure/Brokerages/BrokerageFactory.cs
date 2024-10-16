using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RichillCapital.Binance;
using RichillCapital.Domain.Brokerages;
using RichillCapital.Exchange.Client;
using RichillCapital.Infrastructure.Brokerages.Max;
using RichillCapital.Infrastructure.Brokerages.Rcex;
using RichillCapital.Max;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Brokerages;

internal sealed class BrokerageFactory(
    ILogger<BrokerageFactory> _logger,
    IServiceProvider _serviceProvider)
{
    internal Result<IBrokerage> CreateBrokerage(BrokerageProfile profile) =>
        CreateBrokerage(profile.Provider, profile.Name, profile.Arguments);

    internal Result<IBrokerage> CreateBrokerage(
        string provider,
        string connectionName,
        IReadOnlyDictionary<string, object> arguments)
    {
        var brokerage = provider switch
        {
            "RichillCapital" => Result<IBrokerage>.With(new RcexBrokerage(
                _serviceProvider.GetRequiredService<ILogger<RcexBrokerage>>(),
                _serviceProvider.GetRequiredService<IExchangeRestClient>(),
                connectionName,
                arguments)),

            "Binance" => Result<IBrokerage>.With(new BinanceBrokerage(
                _serviceProvider.GetRequiredService<ILogger<BinanceBrokerage>>(),
                _serviceProvider.GetRequiredService<IBinanceRestClient>(),
                connectionName,
                arguments)),

            "Max" => Result<IBrokerage>.With(new MaxBrokerage(
                _serviceProvider.GetRequiredService<ILogger<MaxBrokerage>>(),
                _serviceProvider.GetRequiredService<IMaxRestClient>(),
                connectionName,
                arguments)),

            _ => Result<IBrokerage>.Failure(BrokerageErrors.NotSupported(provider)),
        };

        _logger.LogInformation(
            "Brokerage connection {connectionName} created for {provider}.",
            connectionName,
            provider);

        return brokerage;
    }
}
