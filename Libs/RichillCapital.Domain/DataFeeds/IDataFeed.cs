using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.DataFeeds;

public interface IDataFeed : IConnection
{
    string Provider { get; }

    Task<Result<IReadOnlyCollection<Instrument>>> ListInstrumentsAsync(CancellationToken cancellationToken = default);
}
