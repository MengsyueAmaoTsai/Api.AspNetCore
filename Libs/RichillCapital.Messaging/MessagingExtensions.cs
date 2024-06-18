using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.Messaging;

public static class MessagingExtensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        return services;
    }
}
