using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using RichillCapital.Http;
using RichillCapital.SharedKernel;

namespace RichillCapital.Exchange.Client;

internal static class HttpResponseExtensions
{
    internal static async Task<Error> ReadAsErrorAsync(
        this HttpResponseMessage httpResponse,
        CancellationToken cancellationToken = default)
    {
        var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

        var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(content)!;

        return httpResponse.GetErrorType() switch
        {
            ErrorType.Validation => Error.Invalid(problemDetails.Title!, problemDetails.Detail!),
            ErrorType.Unauthorized => Error.Unauthorized(problemDetails.Title!, problemDetails.Detail!),
            ErrorType.Forbidden => Error.Forbidden(problemDetails.Title!, problemDetails.Detail!),
            ErrorType.NotFound => Error.NotFound(problemDetails.Title!, problemDetails.Detail!),
            ErrorType.Conflict => Error.Conflict(problemDetails.Title!, problemDetails.Detail!),
            ErrorType.Unexpected => Error.Unexpected(problemDetails.Title!, problemDetails.Detail!),
            _ => Error.Unexpected(problemDetails.Title!, problemDetails.Detail!),
        };
    }
}