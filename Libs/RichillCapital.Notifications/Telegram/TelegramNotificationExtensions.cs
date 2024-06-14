using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.Notifications.Telegram;

public static class TelegramNotificationExtensions
{
    public static IServiceCollection AddTelegramNotification(this IServiceCollection services)
    {
        return services;
    }
}