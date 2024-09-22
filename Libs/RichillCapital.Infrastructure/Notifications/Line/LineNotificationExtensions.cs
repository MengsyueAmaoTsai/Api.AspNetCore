using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Infrastructure.Notifications.Line;

public static class LineNotificationExtensions
{
    public static IServiceCollection AddLineNotification(this IServiceCollection services)
    {
        services
            .AddHttpClient<ILineNotificationService, LineNotificationService>(client =>
            {
                client.BaseAddress = new Uri("https://notify-api.line.me");
                client.Timeout = TimeSpan.FromSeconds(10);
                client.DefaultRequestHeaders.Clear();
            });

        return services;
    }
}
