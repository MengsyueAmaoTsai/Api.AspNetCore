using RichillCapital.SharedKernel;

namespace RichillCapital.Infrastructure.Notifications.Line;

internal static class LineNotificationErrors
{
    internal static Error NotFound(string channelName) =>
        Error.NotFound($"Channel with name '{channelName}' not found.");

    internal static Error Unexpected = Error.Unexpected("Failed to send notification to Line.");
}
