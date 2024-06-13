using FluentValidation;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using RichillCapital.Extensions.Options;
using RichillCapital.Notifications.Line;

namespace RichillCapital.Notifications.Discord;

public static class DiscordNotificationExtensions
{
    public static IServiceCollection AddDiscordNotification(this IServiceCollection services)
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
