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

        var validationResult = Result<GenerateBackTestReportForTradingViewCommand>
            .With(command)
            .Ensure(
                cmd => !string.IsNullOrEmpty(cmd.FileName),
                Error.Invalid("File name is required"))
            .Ensure(
                cmd => cmd.FileName.Contains("_List_of_Trades_"),
                Error.Invalid($"Invalid file name: {command.FileName}"))
            .Ensure(
                cmd => cmd.ContentType == "text/csv",
                Error.Invalid($"Invalid content type: {command.ContentType}"));

        if (validationResult.IsFailure)
        {
            _logger.LogError("{error}", validationResult.Error);

            return ErrorOr<BackTestReportDto>.WithError(validationResult.Error);
        }

        // Read list of trade file 

        return ErrorOr<BackTestReportDto>.With(new BackTestReportDto
        {
            AnnualReturn = 0,
            MaxDrawdown = 0,
            ProfitFactor = 0,
            SharpeRatio = 0,
            Score = 0,
        });
    }
}