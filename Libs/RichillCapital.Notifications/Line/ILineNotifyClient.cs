using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Notifications.Line;

public interface ILineNotifyClient
{
    Task<Result> NotifyAsync(string message, CancellationToken cancellationToken = default);
}
