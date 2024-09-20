using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Abstractions;

public interface IConnection
{
    Guid Id { get; }
    string Name { get; }
    bool IsConnected { get; }

    Task<Result> StartAsync(CancellationToken cancellationToken = default);
    Task<Result> StopAsync(CancellationToken cancellationToken = default);
}