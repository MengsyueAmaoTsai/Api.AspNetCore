using FluentValidation;

namespace RichillCapital.Notifications.Line;

internal sealed record LineNotificationOptions
{
    internal const string SectionKey = "LineNotification";

    public required string AccessToken { get; init; }
}

internal sealed class LineNotificationOptionsValidator :
    AbstractValidator<LineNotificationOptions>
{
    public LineNotificationOptionsValidator()
    {
        RuleFor(options => options.AccessToken)
            .NotEmpty()
            .WithMessage("The access token is required.");
    }
}
