using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.DataFeeds;

public interface IDataFeedManager
{
    Task<Result<IDataFeed>> StartAsync(string connectionName, CancellationToken cancellationToken = default);
    Task<Result<IDataFeed>> StopAsync(string connectionName, CancellationToken cancellationToken = default);
    IReadOnlyCollection<IDataFeed> ListAll();
    Result<IDataFeed> GetByName(string name);
    Task<Result<IDataFeed>> CreateAndStartAsync(
        string provider,
        string name,
        CancellationToken cancellationToken = default);
    Result<IDataFeed> Create(string provider, string name);
    Result Remove(IDataFeed dataFeed);
}
