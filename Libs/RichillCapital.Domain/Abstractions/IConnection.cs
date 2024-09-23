using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Abstractions;

public interface IConnection
{
    string Name { get; }
    ConnectionStatus Status { get; }
    IReadOnlyDictionary<string, object> Arguments { get; }
    DateTimeOffset CreatedTimeUtc { get; }

    Task<Result> StartAsync(CancellationToken cancellationToken = default);
    Task<Result> StopAsync(CancellationToken cancellationToken = default);
}