using System.Diagnostics;
using System.Text;

using Microsoft.IdentityModel.Tokens;

namespace RichillCapital.Api.Middlewares;

internal sealed class RequestDebuggingMiddleware(
    ILogger<RequestDebuggingMiddleware> _logger) :
    IMiddleware
{
    public async Task InvokeAsync(
        HttpContext context,
        RequestDelegate next)
    {
        var method = context.Request.Method;
        var path = context.Request.Path;
        var user = context.User.Identity?.Name ?? "Anonymous";
        var remoteIpAddress = context.Connection.RemoteIpAddress;

        _logger.LogInformation(
            "Incoming request - {method} {path} from {user}@{remoteAddress}",
            method,
            path,
            user,
            remoteIpAddress);

        var stopwatch = Stopwatch.StartNew();

        await next(context);

        var elapsedMilliseconds = (int)stopwatch.Elapsed.TotalMilliseconds;

        var statusCode = context.Response.StatusCode;

        _logger.LogInformation(
            "Outgoing response - {statusCode} for {method} {path}. Elapsed: {elapsed}ms",
            statusCode,
            method,
            path,
            elapsedMilliseconds);

        await LogDetailsIfErrorAsync(context);
    }

    private async Task LogDetailsIfErrorAsync(HttpContext context)
    {
        if (context.Response.StatusCode < 400)
        {
            return;
        }

        var requestHeaders = context.Request.Headers
            .Select(header => $"{header.Key}: {header.Value}").ToArray();

        var responseHeaders = context.Response.Headers
            .Select(header => $"{header.Key}: {header.Value}").ToArray();

        var requestHeaderInfo = requestHeaders.IsNullOrEmpty() ?
            "No request headers" :
            string.Join("\n", requestHeaders);

        var responseHeaderInfo = requestHeaders.IsNullOrEmpty() ?
            "No response headers" :
            string.Join("\n", responseHeaders);

        var queryString = context.Request.QueryString.ToString() ?? "No query string";
        var requestBody = await ReadRequestBodyAsync(context.Request) ?? "No request body";

        _logger.LogError(
            "Error response - Status: {StatusCode}\n" +
            "- Request Headers:\n{RequestHeaders}\n" +
            "- QueryString: {QueryString}\n" +
            "- Request Body: {RequestBody}\n" +
            "- Response Headers:\n{ResponseHeaders}",
            context.Response.StatusCode,
            requestHeaderInfo,
            queryString,
            requestBody,
            responseHeaderInfo);
    }

    private static async Task<string> ReadRequestBodyAsync(HttpRequest request)
    {
        request.EnableBuffering();
        request.Body.Position = 0;

        using var reader = new StreamReader(
            request.Body,
            Encoding.UTF8,
            leaveOpen: true);
        var body = await reader.ReadToEndAsync();

        request.Body.Position = 0;

        return body;
    }
}
