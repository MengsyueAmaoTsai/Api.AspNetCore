using RichillCapital.UseCases.BackTestReports;

namespace RichillCapital.Contracts.BackTestReports;

public sealed record BackTestReportResponse
{
    public required decimal Score { get; init; }
    public required decimal SharpeRatio { get; init; }
    public required decimal MaxDrawdown { get; init; }
    public required decimal ProfitFactory { get; init; }
    public required decimal AnnualReturn { get; init; }
    public required decimal TotalNetProfit { get; init; }
}

public static class BackTestReportResponseMapping
{
    public static BackTestReportResponse ToResponse(this BackTestReportDto dto) =>
        new()
        {
            Score = dto.Score,
            SharpeRatio = dto.SharpeRatio,
            MaxDrawdown = dto.MaxDrawdown,
            ProfitFactory = dto.ProfitFactor,
            AnnualReturn = dto.AnnualReturn,
            TotalNetProfit = dto.TotalNetProfit,
        };
}