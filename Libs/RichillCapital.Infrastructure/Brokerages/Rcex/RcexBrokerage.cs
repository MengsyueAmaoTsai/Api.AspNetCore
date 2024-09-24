using Microsoft.Extensions.Logging;

using RichillCapital.Contracts.Orders;
using RichillCapital.Domain;
using RichillCapital.Domain.Brokerages;
using RichillCapital.Exchange.Client;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Brokerages.Rcex;

internal sealed class RcexBrokerage(
    ILogger<RcexBrokerage> _logger,
    IExchangeRestClient _restClient,
    string name,
    IReadOnlyDictionary<string, object> arguments) :
    Brokerage("RichillCapital", name, arguments)
{
    public override Task<Result<IReadOnlyCollection<Order>>> ListOrdersAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override async Task<Result> StartAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting brokerage connection...");

        if (Status == ConnectionStatus.Active)
        {
            return Result.Failure(BrokerageErrors.AlreadyStarted(Name));
        }

        Status = ConnectionStatus.Active;

        _logger.LogInformation("Brokerage connection started.");

        return await Task.FromResult(Result.Success);
    }

    public override async Task<Result> StopAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Stopping brokerage connection...");

        if (Status == ConnectionStatus.Stopped)
        {
            return Result.Failure(BrokerageErrors.AlreadyStopped(Name));
        }

        Status = ConnectionStatus.Stopped;

        _logger.LogInformation("Brokerage connection stopped.");

        return await Task.FromResult(Result.Success);
    }

    public override async Task<Result> SubmitOrderAsync(
        Symbol symbol,
        TradeType tradeType,
        OrderType orderType,
        TimeInForce timeInForce,
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

        var result = await _restClient.CreateOrderAsync(
            new CreateOrderRequest
            {
                AccountId = "SIM-0000000000000000",
                Symbol = symbol.Value,
                TradeType = tradeType.Name,
                OrderType = orderType.Name,
                TimeInForce = TimeInForce.ImmediateOrCancel.Name,
                Quantity = quantity,
            },
            cancellationToken);

        if (result.IsFailure)
        {
            _logger.LogError("Failed to submit order: {Error}", result.Error);
            return Result.Failure(result.Error);
        }

        return Result.Success;
    }
}
