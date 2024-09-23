using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.DataFeeds;

public interface IDataFeedManager
{
    Task<Result<IDataFeed>> StartAsync(string connectionName, CancellationToken cancellationToken = default);
    Task<Result<IDataFeed>> StopAsync(string connectionName, CancellationToken cancellationToken = default);
    IReadOnlyCollection<IDataFeed> ListAll();
    Result<IDataFeed> GetByName(string name);
    Result<IDataFeed> Create(string provider, string name, IReadOnlyDictionary<string, object> arguments);
    Result Remove(IDataFeed dataFeed);
}
