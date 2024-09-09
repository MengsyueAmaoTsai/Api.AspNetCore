using System.Text;

namespace RichillCapital.Api.Middlewares;

internal sealed class SignalDebuggingMiddleware(
    ILogger<SignalDebuggingMiddleware> _logger) :
    IMiddleware
{
    public async Task InvokeAsync(
        HttpContext context,
        RequestDelegate next)
    {
        if (IsCreateSignalRequest(context))
        {
            context.Request.EnableBuffering();

            using var reader = new StreamReader(
                stream: context.Request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true);

            var body = await reader.ReadToEndAsync();

            context.Request.Body.Position = 0;

            _logger.LogInformation(
                "Received signal: {body}",
                ReplaceCrlf(body));
        }

        await next(context);
    }

    private static string ReplaceCrlf(string text) =>
        text.Replace("\r", "\\r").Replace("\n", "\\n");

    private bool IsCreateSignalRequest(HttpContext context) =>
        context.Request.Method == "POST" && context.Request.Path == "/api/v1/signals";
}