using System.Diagnostics;
using System.Text;

using Microsoft.Extensions.Logging;

namespace RichillCapital.Binance;

internal sealed class RequestDebuggingMessageHandler(
    ILogger<RequestDebuggingMessageHandler> _logger) :
    DelegatingHandler
{
    protected async override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Outgoing request: {Method} {Uri}", request.Method, request.RequestUri);

        var stopwatch = Stopwatch.StartNew();

        var requestInfo = new StringBuilder()
            .AppendLine($"{request.Method} {request.RequestUri} HTTP/1.1")
            .AppendLine("--------------------")
            .AppendLine($"    Headers")
            .AppendLine("--------------------")
            .AppendLine(request.Headers.ToString())
            .ToString();

        _logger.LogInformation(requestInfo);

        var response = await base.SendAsync(request, cancellationToken);

        stopwatch.Stop();

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning(
                "Response for {Method} {Uri} was unsuccessful. Status: {StatusCode}, Reason: {ReasonPhrase} Elapsed: {ElapsedMilliseconds}ms",
                request.Method,
                request.RequestUri,
                response.StatusCode,
                response.ReasonPhrase,
                stopwatch.ElapsedMilliseconds);

            return response;
        }

        _logger.LogInformation(
            "Response for {Method} {Uri} is success. Status: {StatusCode} Elapsed: {ElapsedMilliseconds}ms",
            request.Method,
            request.RequestUri,
            response.StatusCode,
            stopwatch.ElapsedMilliseconds);

        return response;
    }
}