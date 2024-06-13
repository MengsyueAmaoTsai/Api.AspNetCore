using System.Net.Http.Json;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using RichillCapital.Extensions.Options;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Notifications;

public interface IDiscordWebhookClient
{
    Task<Result> SendAsync(
        string username,
        string content,
        CancellationToken cancellationToken = default);
}

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

public static class DiscordNotificationExtensions
{
    public static IServiceCollection AddLineNotification(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(
            typeof(LineNotificationExtensions).Assembly,
            includeInternalTypes: true);

        services
            .AddOptionsWithFluentValidation<DiscordNotificationOptions>(DiscordNotificationOptions.SectionKey);

        using var scope = services
            .BuildServiceProvider()
            .CreateScope();

        var lineNotificationOptions = scope.ServiceProvider
            .GetRequiredService<IOptions<LineNotificationOptions>>()
            .Value;

        services
            .AddHttpClient<IDiscordWebhookClient, DiscordWebhookClient>(client =>
            {
                client.BaseAddress = new Uri("https://discordapp.com");
                client.Timeout = TimeSpan.FromSeconds(5);
            });

        return services;
    }
}

internal sealed record DiscordNotificationOptions
{
    internal const string SectionKey = "DiscordNotification";
}

internal sealed class DiscordNotificationOptionsValidator :
    AbstractValidator<DiscordNotificationOptions>
{
    public DiscordNotificationOptionsValidator()
    {
    }
}