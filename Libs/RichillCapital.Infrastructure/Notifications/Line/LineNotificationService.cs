using System.Text;

using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Notifications.Line;

internal sealed class LineNotificationService(
    ILogger<LineNotificationService> _logger,
    HttpClient _httpClient) :
    ILineNotificationService
{
    private static class ApiRoutes
    {
        internal const string Notify = "api/notify";
    }

    private const string DevelopmentToken = "rTjS0liSNNJSzAtbvYb5YfdyPUazxszoG65nrf9rtC1";

    public async Task<Result> SendAsync(
        string token,
        string message,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return Result.Failure(Error.Invalid($"{nameof(message)} cannot be null or empty"));
        }

        SetBearerToken(token);

        var response = await _httpClient.PostAsync(
            ApiRoutes.Notify,
            CreateContent(message),
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            var error = Error.Unexpected(errorContent);

            _logger.LogError(
                "Failed to send Line notification: {message}. Status code: {statusCode}",
                error.Message,
                response.StatusCode);

            return Result.Failure(error);
        }

        return Result.Success;
    }

    private static StringContent CreateContent(string message) =>
        new($"message=\n{message}", Encoding.UTF8, "application/x-www-form-urlencoded");

    private void SetBearerToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            _logger.LogWarning("Token is not provided, using default token");
            token = DevelopmentToken;
        }

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add(
            "Authorization",
            $"Bearer {token}");
    }
}
