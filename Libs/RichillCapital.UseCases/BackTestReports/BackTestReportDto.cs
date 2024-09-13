namespace RichillCapital.UseCases.BackTestReports;

public sealed record BackTestReportDto
{
    public required decimal Score { get; init; }
    public required decimal SharpeRatio { get; init; }
    public required decimal MaxDrawdown { get; init; }
    public required decimal ProfitFactor { get; init; }
    public required decimal AnnualReturn { get; init; }
}
