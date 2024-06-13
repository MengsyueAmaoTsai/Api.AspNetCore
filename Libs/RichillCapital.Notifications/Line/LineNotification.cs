using FluentValidation;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using RichillCapital.Extensions.Options;

namespace RichillCapital.Notifications.Line;

public static class LineNotificationExtensions
{
    private static readonly Uri BaseAddress = new("https://notify-api.line.me");

    public static IServiceCollection AddLineNotification(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(
            typeof(LineNotificationExtensions).Assembly,
            includeInternalTypes: true);

        services
            .AddOptionsWithFluentValidation<LineNotificationOptions>(LineNotificationOptions.SectionKey);

        using var scope = services
            .BuildServiceProvider()
            .CreateScope();

        var lineNotificationOptions = scope.ServiceProvider
            .GetRequiredService<IOptions<LineNotificationOptions>>()
            .Value;

        services
            .AddHttpClient<ILineNotifyClient, LineNotifyClient>(client =>
            {
                client.BaseAddress = BaseAddress;
                client.Timeout = TimeSpan.FromSeconds(5);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + lineNotificationOptions.AccessToken);
            });

        return services;
    }
}
