using Microsoft.Extensions.Logging;

using RichillCapital.Binance;
using RichillCapital.Domain;
using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel.Monads;

internal sealed class BinanceBrokerage(
    ILogger<BinanceBrokerage> _logger,
    IBinanceRestClient _restClient,
    string name,
    IReadOnlyDictionary<string, object> arguments) :
    Brokerage("Binance", name, arguments)
{
    public override Task<Result<IReadOnlyCollection<Order>>> ListOrdersAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override async Task<Result> StartAsync(CancellationToken cancellationToken = default)
    {
        if (Status == ConnectionStatus.Active)
        {
            return Result.Failure(BrokerageErrors.AlreadyStarted(Name));
        }

        // Ping server and get server time

        var pingResult = await _restClient.PingAsync(cancellationToken);
        var serverTimeResult = await _restClient.GetServerTimeAsync(cancellationToken);

        if (pingResult.IsFailure)
        {
            return Result.Failure(pingResult.Error);
        }

        if (serverTimeResult.IsFailure)
        {
            return Result.Failure(serverTimeResult.Error);
        }

        var serverTimeResponse = serverTimeResult.Value;
        var ping = pingResult.Value;

        Status = ConnectionStatus.Active;

        _logger.LogInformation(
            "Binance Brokerage started at {serverTime}. Ping: {ping}ms",
            serverTimeResponse.ServerTime,
            ping);

        return Result.Success;
    }

    public override Task<Result> StopAsync(CancellationToken cancellationToken = default)
    {
        if (Status == ConnectionStatus.Stopped)
        {
            return Task.FromResult(Result.Failure(BrokerageErrors.AlreadyStopped(Name)));
        }

        Status = ConnectionStatus.Stopped;

        return Task.FromResult(Result.Success);
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
            "Submitting order: {TradeType} {Symbol} {Quantity} @ {OrderType} {timeInForce} with client order ID {ClientOrderId}",
            tradeType,
            symbol,
            quantity,
            orderType,
            timeInForce,
            clientOrderId);

        var newOrderResult = await _restClient.NewOrderAsync(
            symbol.ToBinanceSymbol(),
            tradeType.Name.ToUpperInvariant(),
            orderType.Name.ToUpperInvariant(),
            quantity,
            clientOrderId,
            cancellationToken);

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