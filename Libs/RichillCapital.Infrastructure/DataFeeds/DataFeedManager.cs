using RichillCapital.Domain.DataFeeds;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.DataFeeds;

internal sealed class DataFeedManager(
    IDataFeedCollection _DataFeeds,
    DataFeedFactory _factory) :
    IDataFeedManager
{
    public Result<IDataFeed> GetByName(string name) => _DataFeeds.Get(name);
    public IReadOnlyCollection<IDataFeed> ListAll() => _DataFeeds.All;

    public Result<IDataFeed> Create(string provider, string name, IReadOnlyDictionary<string, object> arguments)
    {
        var createResult = _factory.CreateDataFeed(provider, name, arguments);

        if (createResult.IsFailure)
        {
            return Result<IDataFeed>.Failure(createResult.Error);
        }

        var DataFeed = createResult.Value;
        var addResult = _DataFeeds.Add(DataFeed);

        if (addResult.IsFailure)
        {
            return Result<IDataFeed>.Failure(addResult.Error);
        }

        return Result<IDataFeed>.With(DataFeed);
    }

    public Result Remove(IDataFeed DataFeed) => _DataFeeds.Remove(DataFeed.Name);

    public async Task<Result<IDataFeed>> StartAsync(
        string connectionName,
        CancellationToken cancellationToken = default)
    {
        var DataFeedResult = _DataFeeds.Get(connectionName);

        if (DataFeedResult.IsFailure)
        {
            return Result<IDataFeed>.Failure(DataFeedResult.Error);
        }

        var DataFeed = DataFeedResult.Value;

        var startResult = await DataFeed.StartAsync(cancellationToken);

        if (startResult.IsFailure)
        {
            return Result<IDataFeed>.Failure(startResult.Error);
        }

        return Result<IDataFeed>.With(DataFeed);
    }

    public async Task<Result<IDataFeed>> StopAsync(
        string connectionName,
        CancellationToken cancellationToken = default)
    {
        var DataFeedResult = _DataFeeds.Get(connectionName);

        if (DataFeedResult.IsFailure)
        {
            return Result<IDataFeed>.Failure(DataFeedResult.Error);
        }

        var DataFeed = DataFeedResult.Value;

        var stopResult = await DataFeed.StopAsync(cancellationToken);

        if (stopResult.IsFailure)
        {
            return Result<IDataFeed>.Failure(stopResult.Error);
        }

        return Result<IDataFeed>.With(DataFeed);
    }
}