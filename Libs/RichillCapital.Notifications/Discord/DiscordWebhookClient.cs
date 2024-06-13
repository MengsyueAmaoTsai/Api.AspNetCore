using System.Net.Http.Json;

using Microsoft.Extensions.Logging;

using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Notifications.Discord;

internal sealed class DiscordWebhookClient(
    ILogger<DiscordWebhookClient> _logger,
    HttpClient _httpClient) :
    IDiscordWebhookClient
{
    private const string Path = "/api/webhooks/1250632910323843133/-TPL3vIFrBMgpzeYJRjZcV90vVwBBpls9RCdoP9Q7fbyiZmngM219CCUwetEbX7SrfdI";
    public async Task<Result> SendAsync(
        string username,
        string content,
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync(
            Path,
            new
            {
                username,
                content,
            },
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

            _logger.LogWarning(
                "Failed to send notification. {StatusCode}, {Content}",
                response.StatusCode,
                responseContent);

            return Error
                .Unexpected("DiscordWebhook.Unexpected", "Failed to send notification.")
                .ToResult();
        }

        return Result.Success;
    }
}

