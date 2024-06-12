namespace RichillCapital.Notifications;

public interface ILineNotifyClient
{
    Task NotifyAsync(string message);
}
