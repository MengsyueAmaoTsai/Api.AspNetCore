using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Abstractions;

public interface ILineNotificationService
{
    Task<Result> SendAsync(string message, CancellationToken cancellationToken = default);
    Task<Result> SendAsync(string token, string message, CancellationToken cancellationToken = default);
}