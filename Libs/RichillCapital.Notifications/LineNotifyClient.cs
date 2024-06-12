using System.Text;

using Microsoft.Extensions.Logging;

namespace RichillCapital.Notifications;

internal sealed class LineNotifyClient(
    ILogger<LineNotifyClient> _logger,
    HttpClient _httpClient) :
    ILineNotifyClient
{
    private static class ApiRoutes
    {
        public const string Notify = "api/notify";
    }

    public async Task NotifyAsync(string message)
    {
        var content = new StringContent(
            $"message={message}",
            Encoding.UTF8,
            "application/x-www-form-urlencoded");

        var response = await _httpClient.PostAsync(
            ApiRoutes.Notify,
            content);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning(
                "Failed to send notification. {StatusCode}",
                response.StatusCode);
        }

        response.EnsureSuccessStatusCode();
    }
}