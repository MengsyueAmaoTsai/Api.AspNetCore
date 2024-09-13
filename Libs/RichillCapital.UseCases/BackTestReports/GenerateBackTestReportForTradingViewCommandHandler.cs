using System.Globalization;

using Microsoft.Extensions.Logging;

using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.BackTestReports;

internal sealed class GenerateBackTestReportForTradingViewCommandHandler(
    ILogger<GenerateBackTestReportForTradingViewCommandHandler> _logger) :
    ICommandHandler<GenerateBackTestReportForTradingViewCommand, ErrorOr<BackTestReportDto>>
{
    public async Task<ErrorOr<BackTestReportDto>> Handle(
        GenerateBackTestReportForTradingViewCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Processing file: {fileName}, size: {size}",
            command.FileName,
            command.Length);

        var validationResult = ValidateCommand(command);

        if (validationResult.IsFailure)
        {
            _logger.LogError("{error}", validationResult.Error);
            return ErrorOr<BackTestReportDto>.WithError(validationResult.Error);
        }

        var tradesResult = await ParseTradesAsync(command.FileStream, cancellationToken);

        if (tradesResult.IsFailure)
        {
            return ErrorOr<BackTestReportDto>.WithError(tradesResult.Error);
        }

        var trades = tradesResult.Value;

        var groupedTrade = trades
            .GroupBy(trade => trade.TradeId)
            .ToList();

        var totalNetProfit = groupedTrade
            .Select(group => group.First().Profit)
            .Sum();

        return ErrorOr<BackTestReportDto>.With(new BackTestReportDto
        {
            AnnualReturn = 0,
            MaxDrawdown = 0,
            ProfitFactor = 0,
            SharpeRatio = 0,
            Score = 0,
            TotalNetProfit = totalNetProfit,
        });
    }

    private static Result<GenerateBackTestReportForTradingViewCommand> ValidateCommand(
        GenerateBackTestReportForTradingViewCommand command) =>
        Result<GenerateBackTestReportForTradingViewCommand>
            .With(command)
            .Ensure(
                cmd => !string.IsNullOrEmpty(cmd.FileName),
                Error.Invalid("File name is missing"))
            .Ensure(
                cmd => cmd.Length > 0,
                Error.Invalid("Empty file"))
            .Ensure(
                cmd => cmd.FileName.Contains("_List_of_Trades_"),
                Error.Invalid($"Invalid file name format: {command.FileName}"))
            .Ensure(
                cmd => cmd.ContentType == "text/csv",
                Error.Invalid($"Invalid content type: {command.ContentType}"));
    
    private static async Task<Result<IReadOnlyCollection<TradingViewTradeRecord>>> ParseTradesAsync(
        Stream stream, 
        CancellationToken cancellationToken = default)
    {
        var content = await new StreamReader(stream).ReadToEndAsync(cancellationToken);

        var trades = content
            .Split("\n")
            .Skip(1)
            .Select(line => line.Split(","))
            .Select(TradingViewTradeRecord.FromStrings)
            .ToList();

        return Result<IReadOnlyCollection<TradingViewTradeRecord>>.With(trades);
    }
}

public sealed record TradingViewTradeRecord
{
    public required int TradeId { get; init; }
    public required string Type { get; init; }
    public required string Signal { get; init; }
    public required DateTimeOffset Time { get; init; }
    public required decimal Price { get; init; }
    public required decimal Quantity { get; init; }
    public required decimal Profit { get; init; }

    public static TradingViewTradeRecord FromStrings(string[] columns)
    {
        return new TradingViewTradeRecord
        {
            TradeId = int.Parse(columns[0]),
            Type = columns[1],
            Signal = columns[2],
            Time = DateTimeOffset.ParseExact(columns[3], "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
            Price = decimal.Parse(columns[4], CultureInfo.InvariantCulture),
            Quantity = decimal.Parse(columns[5], CultureInfo.InvariantCulture),
            Profit = decimal.Parse(columns[6], CultureInfo.InvariantCulture),   
        };
    }
}