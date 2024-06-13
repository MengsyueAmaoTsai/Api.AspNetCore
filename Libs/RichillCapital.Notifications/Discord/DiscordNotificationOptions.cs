using FluentValidation;

namespace RichillCapital.Notifications.Discord;

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