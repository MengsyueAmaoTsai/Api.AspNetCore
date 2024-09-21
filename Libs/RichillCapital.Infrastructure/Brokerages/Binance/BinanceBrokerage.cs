using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel.Monads;

internal sealed class BinanceBrokerage(
    ILogger<BinanceBrokerage> _logger,
    string name) :
    Brokerage("Binance", name)
{
    public override async Task<Result> StartAsync(CancellationToken cancellationToken = default)
    {
        if (IsConnected)
        {
            return Result.Failure(BrokerageErrors.AlreadyStarted(Name));
        }

        IsConnected = true;

        _logger.LogInformation("Binance Brokerage started.");

        return Result.Success;
    }

    public override Task<Result> StopAsync(CancellationToken cancellationToken = default)
    {
        if (!IsConnected)
        {
            return Task.FromResult(Result.Failure(BrokerageErrors.AlreadyStopped(Name)));
        }

        IsConnected = false;

        return Task.FromResult(Result.Success);
    }

    public override async Task<Result> SubmitOrderAsync(
        Symbol symbol,
        TradeType tradeType,
        OrderType orderType,
        decimal quantity,
        CancellationToken cancellationToken = default)
    {
        // var binanceSymbol = symbol.Value.Split(':')[1];
        // return await _usdMarginedRestClient.NewOrderAsync(
        //     symbol: binanceSymbol,
        //     side: tradeType.Name.ToUpperInvariant(),
        //     type: orderType.Name.ToUpperInvariant(),
        //     quantity: quantity,
        //     cancellationToken: cancellationToken);
        return Result.Success;
    }
}
