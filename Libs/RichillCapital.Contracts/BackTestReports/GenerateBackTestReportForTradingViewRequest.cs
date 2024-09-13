using Microsoft.AspNetCore.Http;

namespace RichillCapital.Contracts.BackTestReports;

public sealed record GenerateBackTestReportForTradingViewRequest
{
    public required decimal InitialBalance { get; init; }
    public required IFormFile ListOfTradeFile { get; init; }
}
