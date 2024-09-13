using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.BackTestReports;

public sealed record GenerateBackTestReportForTradingViewCommand :
    ICommand<ErrorOr<BackTestReportDto>>
{
    public required string FileName { get; init; }
    public required string ContentType { get; init; }
    public required long Length { get; init; }
    public required Stream FileStream { get; init; }
}
