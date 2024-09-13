using Microsoft.AspNetCore.Http;

namespace RichillCapital.Contracts.BackTestReports;

public sealed record GenerateBackTestReportForTradingViewRequest
{
    public required IFormFile ListOfTradeFile { get; init; }
}
