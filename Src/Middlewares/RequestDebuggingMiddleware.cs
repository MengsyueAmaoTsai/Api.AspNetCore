using System.Diagnostics;

namespace RichillCapital.Api.Middlewares;

internal sealed class RequestDebuggingMiddleware(
    ILogger<RequestDebuggingMiddleware> _logger) :
    IMiddleware
{
    public async Task InvokeAsync(
        HttpContext context,
        RequestDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();

        await next(context);

        var elapsedMilliseconds = (int)stopwatch.Elapsed.TotalMilliseconds;

        var method = context.Request.Method;
        var path = context.Request.Path;
        var statusCode = context.Response.StatusCode;
        var remoteIpAddress = context.Connection.RemoteIpAddress;

        _logger.LogInformation(
            "Status {statusCode} for {method} {path} from {address}. Elapsed: {elapsed} (ms)",
            statusCode,
            ReplaceCrlf(method),
            ReplaceCrlf(path),
            remoteIpAddress,
            elapsedMilliseconds);

        await LogErrorIfFailureAsync(context.Response);
    }

    private static string ReplaceCrlf(string text) =>
        text.Replace("\r", "\\r").Replace("\n", "\\n");

    private async Task LogErrorIfFailureAsync(
        HttpResponse httpResponse)
    {
        static bool IsErrorResponse(HttpResponse httpResponse) =>
            httpResponse.StatusCode != 200 &&
            httpResponse.ContentType == "application/problem+json";

        if (IsErrorResponse(httpResponse))
        {
            var problemDetailsResult = await httpResponse.ReadProblemDetailsAsync(default);

            if (problemDetailsResult.IsSuccess)
            {
                _logger.LogError(
                    "Problem details: {problemDetails}",
                    ReplaceCrlf(problemDetailsResult.Value));
            }
        }
    }
}
