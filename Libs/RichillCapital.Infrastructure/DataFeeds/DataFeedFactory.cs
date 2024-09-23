using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RichillCapital.Domain.DataFeeds;
using RichillCapital.Infrastructure.DataFeeds.Max;
using RichillCapital.Max;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.DataFeeds;

internal sealed class DataFeedFactory(
    ILogger<DataFeedFactory> _logger,
    IServiceProvider _serviceProvider)
{
    internal Result<IDataFeed> CreateDataFeed(DataFeedProfile profile) =>
        CreateDataFeed(profile.Provider, profile.Name, profile.Arguments);

    internal Result<IDataFeed> CreateDataFeed(
        string provider,
        string connectionName,
        IReadOnlyDictionary<string, object> arguments)
    {
        var DataFeed = provider switch
        {
            "Max" => Result<IDataFeed>.With(new MaxDataFeed(
                _serviceProvider.GetRequiredService<ILogger<MaxDataFeed>>(),
                _serviceProvider.GetRequiredService<IMaxRestClient>(),
                connectionName,
                arguments)),

            _ => Result<IDataFeed>.Failure(DataFeedErrors.NotSupported(provider)),
        };

        _logger.LogInformation(
            "DataFeed connection {connectionName} created for {provider}.",
            connectionName,
            provider);

        return DataFeed;
    }
}
