using System.Diagnostics;
using System.Text;
using System.Web;

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
        var stopwatch = Stopwatch.StartNew();
        var requestInfo = await GetRequestInfoAsync(request, cancellationToken);
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

    private static async Task<string> GetRequestInfoAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken = default)
    {
        var requestInfo = new StringBuilder()
            .AppendLine($"{request.Method} {request.RequestUri} HTTP/1.1")
            .AppendLine("--------------------")
            .AppendLine($"    Headers")
            .AppendLine("--------------------")
            .AppendLine(request.Headers.ToString());

        if (request.Method == HttpMethod.Post || request.Method == HttpMethod.Put)
        {
            if (request.Content != null)
            {
                var content = await request.Content.ReadAsStringAsync(cancellationToken);

                requestInfo
                    .AppendLine("--------------------")
                    .AppendLine("    Body")
                    .AppendLine("--------------------")
                    .AppendLine(ParseFormData(content));
            }
        }

        return requestInfo.ToString();
    }

    private static string ParseFormData(string formData)
    {
        var parsedData = HttpUtility.ParseQueryString(formData);
        var formattedData = new StringBuilder();

        foreach (var key in parsedData.AllKeys)
        {
            formattedData.AppendLine($"{key}: {parsedData[key]}");
        }

        return formattedData.ToString();
    }
}