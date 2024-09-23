namespace RichillCapital.UseCases.Accounts;

public sealed record AccountPerformanceDto
{
    public required int TotalNumberOfTrades { get; init; }
    public required int NumberOfWinningTrades { get; init; }
    public required int NumberOfLosingTrades { get; init; }

    public required decimal TotalProfitLoss { get; init; }
    public required decimal TotalProfit { get; init; }
    public required decimal TotalLoss { get; init; }

    public required decimal AverageProfitLoss { get; init; }
    public required decimal AverageProfit { get; init; }
    public required decimal AverageLoss { get; init; }

    public required decimal LargeProfit { get; init; }
    public required decimal LargeLoss { get; init; }

    public required decimal TotalCommission { get; init; }
    public required decimal TotalTax { get; init; }
    public required decimal TotalSwap { get; init; }

    public required decimal ProfitLossRatio { get; init; }
    public required decimal WinLossRatio { get; init; }
    public required decimal WinRate { get; init; }
    public required decimal ProfitFactor { get; init; }

    public required decimal MaximumClosedTradeDrawdown { get; init; }
    public required decimal ProfitToMaxClosedTradeDrawdownRatio { get; init; }
}