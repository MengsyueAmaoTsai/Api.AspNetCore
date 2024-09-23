using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.DataFeeds;
using RichillCapital.Max;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.DataFeeds.Max;

internal sealed class MaxDataFeed(
    ILogger<MaxDataFeed> _logger,
    IMaxRestClient _restClient,
    string name) :
    DataFeed("Max", name)
{
    public override async Task<Result> StartAsync(CancellationToken cancellationToken = default)
    {
        // Check server time
        var serverTimeResult = await _restClient.GetServerTimeAsync(cancellationToken);

        if (serverTimeResult.IsFailure)
        {
            return Result.Failure(serverTimeResult.Error);
        }

        _logger.LogInformation(
            "Server time: {time}",
            serverTimeResult.Value);

        // User info
        var userInfo = await _restClient.GetUserInfoAsync(cancellationToken);

        if (userInfo.IsFailure)
        {
            return Result.Failure(userInfo.Error);
        }

        _logger.LogInformation(
            "User info: {info}",
            userInfo.Value);

        Status = ConnectionStatus.Active;

        return await OnStartedAsync(cancellationToken);
    }

    public override Task<Result> StopAsync(CancellationToken cancellationToken = default)
    {
        Status = ConnectionStatus.Stopped;

        return Task.FromResult(Result.Success);
    }

    private async Task<Result> OnStartedAsync(CancellationToken cancellationToken = default)
    {
        return Result.Success;
    }

    public override async Task<Result<IReadOnlyCollection<Instrument>>> ListInstrumentsAsync(CancellationToken cancellationToken = default)
    {
        return Result<IReadOnlyCollection<Instrument>>.With([]);
    }
}