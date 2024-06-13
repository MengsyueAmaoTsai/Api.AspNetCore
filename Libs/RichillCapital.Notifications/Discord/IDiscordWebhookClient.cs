using FluentValidation;

using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Notifications.Discord;

public interface IDiscordWebhookClient
{
    Task<Result> SendAsync(
        string username,
        string content,
        CancellationToken cancellationToken = default);
}

