using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Brokerages.Max;

internal sealed class MaxBrokerage(
    ILogger<MaxBrokerage> _logger,
    string name) :
    Brokerage("Max", name)
{
    public override async Task<Result> StartAsync(CancellationToken cancellationToken = default)
    {
        IsConnected = true;

        return await Task.FromResult(Result.Success);
    }

    public override Task<Result> StopAsync(CancellationToken cancellationToken = default)
    {
        IsConnected = false;

        return Task.FromResult(Result.Success);
    }

    public override Task<Result> SubmitOrderAsync(Symbol symbol, TradeType tradeType, OrderType orderType, decimal quantity, string clientOrderId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Submitting order: {TradeType} {Symbol} {Quantity} @ {OrderType} with client order ID {ClientOrderId}",
            tradeType,
            symbol,
            quantity,
            orderType,
            clientOrderId);

        return Task.FromResult(Result.Success);
    }
}