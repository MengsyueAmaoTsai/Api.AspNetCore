using RichillCapital.SharedKernel;

namespace RichillCapital.Binance.Spot;

internal static class BinanceSpotErrors
{
    internal static Error MapError(BinanceErrorResponse errorResponse) =>
        Error.Invalid(
            MapErrorCode(errorResponse),
            errorResponse.Message);

    internal static string MapErrorCode(BinanceErrorResponse errorResponse)
    {
        var suffix = errorResponse.Code switch
        {
            -1000 => "Unknown",
            -1001 => "Disconnected",
            -1002 => "Unauthorized",
            -1003 => "TooManyRequests",
            -1006 => "UnexpectedResponse",
            -1007 => "Timeout",
            -1008 => "ServerBusy",
            -1013 => "InvalidMessage",
            -1014 => "UnknownOrderComposition",
            -1015 => "TooManyOrders",
            -1016 => "ServiceShuttingDown",
            -1020 => "UnsupportedOperation",
            -1021 => "InvalidTimestamp",
            -1022 => "InvalidSignature",

            -1131 => "InvalidRecvWindow",

            -2015 => "RejectedMbxKey",
            _ => throw new NotImplementedException(
                $"Error code {errorResponse.Code} with message: '{errorResponse.Message}' is not implemented"),
        };

        return $"Binance.{suffix}";
    }
}