using System.Diagnostics;
using System.Text;

using Microsoft.Extensions.Logging;

namespace RichillCapital.Http;

internal sealed class DefaultRequestDebuggingMessageHandler(
    ILogger<DefaultRequestDebuggingMessageHandler> _logger) :
    DelegatingHandler
{
    private const string NoHeaders = "No headers";
    private const string NoRequestBody = "No request body";
    private const string NoResponseBody = "No response body";
    private const string UnknownUri = "Unknown URI";

    protected async override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();

        var method = request.Method.Method;
        var url = request.RequestUri?.ToString() ?? UnknownUri;
        var requestBodyInfo = await ReadRequestBodyAsync(request);

        _logger.LogInformation("Sending request - {Method} {Url}", method, url);

        var requestHeaders = request.Headers
            .Select(header => $"{header.Key}: {string.Join(", ", header.Value)}")
            .ToArray();

        var requestHeaderInfo = requestHeaders.Any() ?
            string.Join("\n", requestHeaders) :
            NoHeaders;

        HttpResponseMessage response;

        try
        {
            response = await base.SendAsync(request, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while sending request - {method} {url}", method, url);
            throw;
        }

        var statusCode = (int)response.StatusCode;
        var elapsedMilliseconds = (int)stopwatch.Elapsed.TotalMilliseconds;

        _logger.LogInformation(
            "Received response - {statusCode} for {method} {url}. Elapsed: {elapsed}ms",
            statusCode, method, url, elapsedMilliseconds);

        await LogHttpContextDetailsIfError(response, requestHeaderInfo, requestBodyInfo);

        return response;
    }

    private static async Task<string> ReadRequestBodyAsync(HttpRequestMessage request)
    {
        if (request.Content == null)
        {
            return NoRequestBody;
        }

        var requestBody = await request.Content.ReadAsStringAsync();

        return string.IsNullOrEmpty(requestBody) ? NoRequestBody : requestBody;
    }

    private static async Task<string> ReadResponseBodyAsync(HttpResponseMessage response)
    {
        if (response.Content == null)
        {
            return NoResponseBody;
        }

        var responseBody = await response.Content.ReadAsStringAsync();

        return string.IsNullOrEmpty(responseBody) ? NoResponseBody : responseBody;
    }

    private async Task LogHttpContextDetailsIfError(
        HttpResponseMessage response,
        string requestHeaderInfo,
        string requestBodyInfo)
    {
        var responseBodyInfo = await ReadResponseBodyAsync(response);

        if ((int)response.StatusCode >= 400)
        {
            var responseHeaders = response.Headers
                .Select(header => $"{header.Key}: {string.Join(", ", header.Value)}")
                .ToArray();

            var responseHeaderInfo = responseHeaders.Any() ? string.Join("\n", responseHeaders) : NoHeaders;

            var logMessage = new StringBuilder()
                .AppendLine("----- Request Details -----")
                .AppendLine($"Request Headers:\n{requestHeaderInfo}")
                .AppendLine($"Request Body:\n{requestBodyInfo ?? NoRequestBody}")
                .AppendLine("----- Response Details -----")
                .AppendLine($"Response Headers:\n{responseHeaderInfo}")
                .AppendLine($"Response Body:\n{responseBodyInfo ?? NoResponseBody}")
                .ToString();

            _logger.LogWarning(logMessage);
        }
    }
}
