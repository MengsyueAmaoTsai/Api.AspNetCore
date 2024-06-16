using System.Diagnostics;
using System.Security.Claims;

using RichillCapital.UseCases.Common;

namespace RichillCapital.Api.Middlewares;

internal sealed class DebuggingRequestMiddleware(
    ILogger<DebuggingRequestMiddleware> _logger,
    ICurrentUser _currentUser) : 
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

        var userId = _currentUser.IsAuthenticated ? 
            _currentUser.Id.Value : 
            "Anonymous";

        _logger.LogInformation(
            "Request {method} {path} from {address}. Status: {statusCode} for user '{userId}'. Elapsed: {elapsed} (ms)", 
            ReplaceCrlf(method),
            ReplaceCrlf(path),
            remoteIpAddress,
            statusCode,
            userId,
            elapsedMilliseconds);
    }

    private static string ReplaceCrlf(string text) => 
        text.Replace("\r", "\\r").Replace("\n", "\\n");
}