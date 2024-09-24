using RichillCapital.Domain;
using RichillCapital.Max.Contracts;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.DataFeeds.Max;

internal sealed partial class MaxDataFeed
{
    private async Task<Result> OnStartedAsync(CancellationToken cancellationToken = default)
    {
        return Result.Success;
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
                contractUnit: ContractUnitForCryptoCurrency,
                createdTimeUtc: DateTimeOffset.UtcNow);

        if (errorOrInstrument.HasError)
        {
            return Result<Instrument>.Failure(errorOrInstrument.Errors.First());
        }

        return Result<Instrument>.With(errorOrInstrument.Value);
    }
}