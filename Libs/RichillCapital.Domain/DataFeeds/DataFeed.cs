using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.DataFeeds;

public abstract class DataFeed(
    string provider,
    string name,
    IReadOnlyDictionary<string, object> arguments) :
    IDataFeed
{
    public string Provider { get; private init; } = provider;
    public string Name { get; private init; } = name;
    public IReadOnlyDictionary<string, object> Arguments { get; private init; } = arguments;
    public ConnectionStatus Status { get; protected set; } = ConnectionStatus.Stopped;

    public DateTimeOffset CreatedTimeUtc { get; private init; } = DateTimeOffset.UtcNow;

    public abstract Task<Result<IReadOnlyCollection<Instrument>>> ListInstrumentsAsync(CancellationToken cancellationToken = default);
    public abstract Task<Result> StartAsync(CancellationToken cancellationToken = default);
    public abstract Task<Result> StopAsync(CancellationToken cancellationToken = default);
}
