using RichillCapital.Domain.DataFeeds;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.DataFeeds;

internal sealed class DataFeedCollection() :
    IDataFeedCollection
{
    private readonly Dictionary<string, IDataFeed> _DataFeeds = [];

    public IReadOnlyCollection<IDataFeed> All => _DataFeeds.Values;

    public Result Add(IDataFeed DataFeed)
    {
        if (_DataFeeds.ContainsKey(DataFeed.Name))
        {
            return Result.Failure(DataFeedErrors.AlreadyExists(DataFeed.Name));
        }

        _DataFeeds.Add(DataFeed.Name, DataFeed);

        return Result.Success;
    }

    public Result<IDataFeed> Get(string name)
    {
        if (!_DataFeeds.ContainsKey(name))
        {
            return Result<IDataFeed>.Failure(DataFeedErrors.NotFound(name));
        }

        return Result<IDataFeed>.With(_DataFeeds[name]);
    }

    public Result Remove(string name)
    {
        if (!_DataFeeds.ContainsKey(name))
        {
            return Result.Failure(DataFeedErrors.NotFound(name));
        }

        _DataFeeds.Remove(name);

        return Result.Success;
    }
}
