using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Notifications;

public interface ILineNotifyClient
{
    Task<Result> NotifyAsync(string message, CancellationToken cancellationToken = default);
}
