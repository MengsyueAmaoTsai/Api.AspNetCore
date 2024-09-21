using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Brokerages;
using RichillCapital.Infrastructure.Brokerages.Rcex;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Brokerages;

internal sealed class BrokerageFactory(
    IServiceProvider _serviceProvider)
{
    internal Result<IBrokerage> CreateBrokerage(
        string provider,
        string connectionName) =>
        provider switch
        {
            "RichillCapital" => Result<IBrokerage>.With(new RcexBrokerage(
                _serviceProvider.GetRequiredService<ILogger<RcexBrokerage>>(),
                connectionName)),

            "Binance" => Result<IBrokerage>.With(new BinanceBrokerage(
                _serviceProvider.GetRequiredService<ILogger<BinanceBrokerage>>(),
                connectionName)),

            _ => Result<IBrokerage>.Failure(Error.Invalid(
                "Brokerages.NotSupported",
                $"Brokerage connection {connectionName} is not supported."))
        };
}
