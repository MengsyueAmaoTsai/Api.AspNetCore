using RichillCapital.SharedKernel;

namespace RichillCapital.Binance;

internal static class BinanceErrors
{
    private const string ErrorCodePrefix = "Binance";

    internal static Error Create(ErrorType type, BinanceErrorResponse response) =>
        Create(type, ConvertErrorCode(response.Code), response.Message);

    private static Error Create(ErrorType type, string code, string message) =>
        type switch
        {
            ErrorType.Validation => Error.Invalid(code, message),
            ErrorType.Unauthorized => Error.Unauthorized(code, message),
            ErrorType.Forbidden => Error.Forbidden(code, message),
            ErrorType.NotFound => Error.NotFound(code, message),
            ErrorType.Conflict => Error.Conflict(code, message),
            ErrorType.Unexpected => Error.Unexpected(code, message),
            _ => Error.Unexpected(code, message)
        };

    private static string ConvertErrorCode(int binanceErrorCode)
    {
        var suffix = binanceErrorCode switch
        {
            -1000 => "Unknown",
            -1102 => "MandatoryParamEmptyOrMalformed",
            -1121 => "BadSymbol",

            -1022 => "InvalidSignature",
            -2014 => "BadApiKeyFormat",

            -2019 => "MarginNotSufficient", // USD-M only
            _ => throw new NotImplementedException($"{nameof(binanceErrorCode)} for {binanceErrorCode} is not defined."),
        };

        return $"{ErrorCodePrefix}.{suffix}";
    }
}