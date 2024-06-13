using System.Text;

using Microsoft.Extensions.Logging;

using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Notifications.Line;

internal sealed class LineNotifyClient(
    ILogger<LineNotifyClient> _logger,
    HttpClient _httpClient) :
    ILineNotifyClient
{
    private static class ApiRoutes
    {
        public const string Notify = "api/notify";
    }

    public async Task<Result> NotifyAsync(string message, CancellationToken cancellationToken = default)
    {
        var content = new StringContent(
            $"message={message}",
            Encoding.UTF8,
            "application/x-www-form-urlencoded");

        var response = await _httpClient.PostAsync(
            ApiRoutes.Notify,
            content,
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning(
                "Failed to send notification. {StatusCode}",
                response.StatusCode);

            return Error
                .Unexpected("LineNotify.Unexpected", "Failed to send notification.")
                .ToResult();
        }

        return Result.Success;
    }
}