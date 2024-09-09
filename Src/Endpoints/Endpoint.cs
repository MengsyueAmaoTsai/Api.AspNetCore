using Microsoft.AspNetCore.Mvc;

using RichillCapital.SharedKernel;

namespace RichillCapital.Api.Endpoints;

[ApiController]
[Produces("application/json")]
public abstract class Endpoint : ControllerBase
{
    protected virtual ActionResult HandleFailure(IEnumerable<Error> errors)
    {
        var error = errors.Any(error => error.Type == ErrorType.Validation) ?
            errors.First(error => error.Type == ErrorType.Validation) :
            errors.First();

        return HandleFailure(error);
    }

    protected virtual ActionResult HandleFailure(Error error) =>
        Problem(
            detail: error.Message,
            instance: null,
            statusCode: error.StatusCode(),
            title: error.Code,
            type: error.RfcUrl());
}

internal static class ErrorExtensions
{
    internal static int StatusCode(this Error error) =>
        error.Type switch
        {
            ErrorType.Validation => 400,
            ErrorType.Unauthorized => 401,
            ErrorType.Forbidden => 403,
            ErrorType.NotFound => 404,
            ErrorType.Conflict => 409,
            ErrorType.Unexpected => 500,
            ErrorType.Unavailable => 503,
            _ => 500
        };

    internal static string RfcUrl(this Error error) =>
        error.Type switch
        {
            ErrorType.Validation => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            ErrorType.Unauthorized => "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
            ErrorType.Forbidden => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3",
            ErrorType.NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            ErrorType.Conflict => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
            ErrorType.Unexpected => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            ErrorType.Unavailable => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.4",
            _ => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
        };
}