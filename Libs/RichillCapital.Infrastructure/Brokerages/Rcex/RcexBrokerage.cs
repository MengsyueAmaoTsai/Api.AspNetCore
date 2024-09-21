using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Brokerages;
using RichillCapital.Exchange.Client;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Brokerages.Rcex;

internal sealed class RcexBrokerage(
    ILogger<RcexBrokerage> _logger,
    IExchangeRestClient _restClient,
    string name) :
    Brokerage("RichillCapital", name)
{
    public override async Task<Result> StartAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting brokerage connection...");

        if (IsConnected)
        {
            return Result.Failure(BrokerageErrors.AlreadyStarted(Name));
        }

        IsConnected = true;

        _logger.LogInformation("Brokerage connection started.");

        return await Task.FromResult(Result.Success);
    }

    public override async Task<Result> StopAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Stopping brokerage connection...");

        if (!IsConnected)
        {
            return Result.Failure(BrokerageErrors.AlreadyStopped(Name));
        }

        IsConnected = false;

        _logger.LogInformation("Brokerage connection stopped.");

        return await Task.FromResult(Result.Success);
    }

    public override async Task<Result> SubmitOrderAsync(
        Symbol symbol,
        TradeType tradeType,
        OrderType orderType,
        decimal quantity,
        string clientOrderId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Submitting order: {TradeType} {Symbol} {Quantity} @ {OrderType} with client order ID {ClientOrderId}",
            tradeType,
            symbol,
            quantity,
            orderType,
            clientOrderId);

        var result = await _restClient.CreateOrderAsync(cancellationToken);

        if (result.IsFailure)
        {
            _logger.LogError("Failed to submit order: {Error}", result.Error);
            return Result.Failure(result.Error);
        }

        return Result.Success;
    }
}
