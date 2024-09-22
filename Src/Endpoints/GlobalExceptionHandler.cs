using System.Diagnostics;
using System.Net;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Infrastructure.Logging;
using RichillCapital.SharedKernel;

namespace RichillCapital.Api.Endpoints;

internal sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> _logger) :
    IExceptionHandler
{
    private const string InternalServerErrorType = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1";
    private const string ContentType = "application/problem+json";

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var response = httpContext.Response;

        _logger.LogError(
            exception,
            "[{Ticks}-{ThreadId}]",
            DateTime.UtcNow.Ticks,
            Environment.CurrentManagedThreadId);

        var error = Error.Unexpected(exception.Message);

        var problemDetails = new ProblemDetails
        {
            Title = error.Code,
            Detail = error.Message,
            Status = (int)HttpStatusCode.InternalServerError,
            Type = InternalServerErrorType,
            Instance = httpContext.Request.Path,
        };

        problemDetails.Extensions.Add("message", exception.Message);
        problemDetails.Extensions.Add("traceId", Activity.Current?.GetTraceId());

        response.ContentType = ContentType;
        response.StatusCode = problemDetails.Status.Value;

        await response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}