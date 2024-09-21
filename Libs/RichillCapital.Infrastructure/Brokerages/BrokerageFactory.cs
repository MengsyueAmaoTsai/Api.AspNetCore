using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RichillCapital.Binance;
using RichillCapital.Domain.Brokerages;
using RichillCapital.Exchange.Client;
using RichillCapital.Infrastructure.Brokerages.Max;
using RichillCapital.Infrastructure.Brokerages.Rcex;
using RichillCapital.Max;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Brokerages;

internal sealed class BrokerageFactory(
    ILogger<BrokerageFactory> _logger,
    IServiceProvider _serviceProvider)
{
    internal Result<IBrokerage> CreateBrokerage(
        string provider,
        string connectionName)
    {
        var brokerage = provider switch
        {
            "RichillCapital" => Result<IBrokerage>.With(new RcexBrokerage(
                _serviceProvider.GetRequiredService<ILogger<RcexBrokerage>>(),
                _serviceProvider.GetRequiredService<IExchangeRestClient>(),
                connectionName)),

            "Binance" => Result<IBrokerage>.With(new BinanceBrokerage(
                _serviceProvider.GetRequiredService<ILogger<BinanceBrokerage>>(),
                _serviceProvider.GetRequiredService<IBinanceRestClient>(),
                connectionName)),

            "Max" => Result<IBrokerage>.With(new MaxBrokerage(
                _serviceProvider.GetRequiredService<ILogger<MaxBrokerage>>(),
                _serviceProvider.GetRequiredService<IMaxRestClient>(),
                connectionName)),

            _ => Result<IBrokerage>.Failure(Error.Invalid(
                "Brokerages.NotSupported",
                $"Brokerage connection {connectionName} is not supported."))
        };

        _logger.LogInformation(
            "Brokerage connection {connectionName} created for {provider}.",
            connectionName,
            provider);

        return brokerage;
    }
}
