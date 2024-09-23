using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Errors;

public static class SignalErrors
{
    public static Error IllegalOrigin(string origin) =>
        Error.Invalid("Signals.IllegalOrigin", $"Illegal origin: {origin}");

    public static Error InvalidTime(DateTimeOffset time) =>
        Error.Invalid("Signals.InvalidTime", $"Invalid time: {time}");

    public static readonly Error TimeInFuture =
        Error.Invalid("Signals.TimeInFuture", "Time cannot be in the future");

    public static Error InvalidTradeType(string tradeType) =>
        Error.Invalid("Signals.InvalidTradeType", $"Invalid trade type: {tradeType}");

    public static Error InvalidOrderType(string orderType) =>
        Error.Invalid("Signals.InvalidOrderType", $"Invalid order type: {orderType}");

    public static Error InvalidQuantity =
        Error.Invalid("Signals.InvalidQuantity", "Quantity must be greater than 0");

    public static Error NotFound(SignalId signalId) =>
        Error.NotFound(
            "Signals.NotFound",
            $"Signal with id {signalId} not found");

    public static Error SourceNotFound(SignalSourceId sourceId) =>
        Error.NotFound(
            "Signals.SourceNotFound",
            $"Signal source with id {sourceId} not found");
}