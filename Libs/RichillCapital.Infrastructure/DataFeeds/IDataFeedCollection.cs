using RichillCapital.Domain.DataFeeds;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.DataFeeds;

public interface IDataFeedCollection
{
    IReadOnlyCollection<IDataFeed> All { get; }
    Result Add(IDataFeed DataFeed);
    Result<IDataFeed> Get(string name);
    Result Remove(string name);
}
