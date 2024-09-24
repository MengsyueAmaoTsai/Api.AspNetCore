using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.DataFeeds;
using RichillCapital.Infrastructure.Brokerages.Max;
using RichillCapital.Max;
using RichillCapital.Max.Contracts;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.DataFeeds.Max;

internal sealed class MaxDataFeed(
    ILogger<MaxDataFeed> _logger,
    IMaxRestClient _restClient,
    string name,
    IReadOnlyDictionary<string, object> arguments) :
    DataFeed("Max", name, arguments)
{
    private readonly MaxSymbolMapper _symbolMapper = new();

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

    public override async Task<Result<IReadOnlyCollection<Instrument>>> ListInstrumentsAsync(
        CancellationToken cancellationToken = default)
    {
        var marketsResult = await _restClient.ListMarketsAsync(cancellationToken);

        if (marketsResult.IsFailure)
        {
            return Result<IReadOnlyCollection<Instrument>>.Failure(marketsResult.Error);
        }

        var instrumentResults = marketsResult.Value
            .Select(MapToInstrument)
            .ToList();

        if (instrumentResults.Any(instrument => instrument.IsFailure))
        {
            return Result<IReadOnlyCollection<Instrument>>.Failure(
                instrumentResults.First(instrument => instrument.IsFailure).Error);
        }

        var instruments = instrumentResults
            .Select(instrument => instrument.Value)
            .ToList();

        return Result<IReadOnlyCollection<Instrument>>.With(instruments);
    }

    private Result<Instrument> MapToInstrument(MaxMarketResponse market)
    {
        var symbol = _symbolMapper.FromExternalSymbol(market.Id);
        var maybeQuoteCurrency = Currency.FromName(market.QuoteUnit, ignoreCase: true);

        if (maybeQuoteCurrency.IsNull)
        {
            return Result<Instrument>.Failure(Error.Invalid($"Cannot map quote currency: {market.QuoteUnit}"));
        }

        var errorOrInstrument = Instrument
            .Create(
                symbol: symbol,
                description: market.Id,
                type: InstrumentType.CryptoCurrency,
                quoteCurrency: maybeQuoteCurrency.Value,
                contractUnit: 1,
                createdTimeUtc: DateTimeOffset.UtcNow);

        if (errorOrInstrument.HasError)
        {
            return Result<Instrument>.Failure(errorOrInstrument.Errors.First());
        }

        return Result<Instrument>.With(errorOrInstrument.Value);
    }
}
