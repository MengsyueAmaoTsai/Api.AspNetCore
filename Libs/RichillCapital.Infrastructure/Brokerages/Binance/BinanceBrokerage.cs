using Microsoft.Extensions.Logging;

using RichillCapital.Binance;
using RichillCapital.Domain;
using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel.Monads;

internal sealed class BinanceBrokerage(
    ILogger<BinanceBrokerage> _logger,
    IBinanceRestClient _restClient,
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
        _logger.LogInformation(
            "Submitting order: {TradeType} {Symbol} {Quantity} @ {OrderType}",
            tradeType,
            symbol,
            quantity,
            orderType);

        var newOrderResult = await _restClient.NewOrderAsync(
            symbol.ToBinanceSymbol(),
            tradeType.Name.ToUpperInvariant(),
            orderType.Name.ToUpperInvariant(),
            quantity);

        if (newOrderResult.IsFailure)
        {
            return Result.Failure(newOrderResult.Error);
        }

        return Result.Success;
    }
}

internal static class SymbolExtensions
{
    internal static string ToBinanceSymbol(this Symbol symbol)
    {
        var parts = symbol.Value.Split(':');
        var exchangePart = parts[0];
        var symbolPart = parts[1];

        if (exchangePart != "BINANCE")
        {
            throw new ArgumentException($"Invalid exchange part: {exchangePart}");
        }

        if (symbolPart.Contains('.'))
        {
            return symbolPart.Split('.').First();
        }

        return symbolPart;
    }
}