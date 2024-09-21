using RichillCapital.Max.Contracts;
using RichillCapital.SharedKernel;

namespace RichillCapital.Max;

internal static class MaxErrors
{
    private const string ErrorCodePrefix = "Max";
    internal static Error Create(ErrorType type, MaxErrorResponse response) =>
        Create(type, ConvertErrorCode(response.Error.Code), response.Error.Message);

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

    private static string ConvertErrorCode(int maxErrorCode)
    {
        var suffix = maxErrorCode switch
        {
            2001 => "Error",
            _ => throw new NotImplementedException($"{nameof(maxErrorCode)} for {maxErrorCode} is not defined."),
        };

        return $"{ErrorCodePrefix}.{suffix}";
    }
}