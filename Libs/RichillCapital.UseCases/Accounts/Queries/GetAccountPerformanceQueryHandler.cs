using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Accounts.Queries;

internal sealed class GetAccountPerformanceQueryHandler(
    IReadOnlyRepository<Account> _accountRepository,
    IReadOnlyRepository<Trade> _tradeRepository) :
    IQueryHandler<GetAccountPerformanceQuery, ErrorOr<AccountPerformanceDto>>
{
    public async Task<ErrorOr<AccountPerformanceDto>> Handle(
        GetAccountPerformanceQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = AccountId.From(query.AccountId);

        if (validationResult.IsFailure)
        {
            return ErrorOr<AccountPerformanceDto>.WithError(validationResult.Error);
        }

        var id = validationResult.Value;

        if (!await _accountRepository.AnyAsync(a => a.Id == id))
        {
            return ErrorOr<AccountPerformanceDto>.WithError(AccountErrors.NotFound(id));
        }

        var closedTrades = await _tradeRepository.ListAsync(t => t.AccountId == id, cancellationToken);

        var winningTrades = closedTrades.Where(t => t.IsWinningTrade()).ToList();
        var losingTrades = closedTrades.Where(t => t.IsLosingTrade()).ToList();

        return ErrorOr<AccountPerformanceDto>.With(GenerateAccountPerformance(closedTrades));
    }

    private static AccountPerformanceDto GenerateAccountPerformance(IEnumerable<Trade> closedTrades)
    {
        var lastPeakTime = DateTimeOffset.MinValue;
        var isInDrawdown = false;
        var maxTotalProfitLoss = 0m;

        var (totalNumberOfTrades, numberOfWinningTrades, numberOfLosingTrades) = (0, 0, 0);
        var (totalProfitLoss, totalProfit, totalLoss) = (0m, 0m, 0m);
        var (totalCommission, totalTax, totalSwap) = (0m, 0m, 0m);
        var (averageProfitLoss, averageProfit, averageLoss) = (0m, 0m, 0m);
        var (largestProfit, largestLoss) = (0m, 0m);
        var maxClosedTradeDrawdown = 0m;

        foreach (var t in closedTrades)
        {
            if (lastPeakTime == DateTimeOffset.MinValue)
            {
                lastPeakTime = t.EntryTimeUtc;
            }

            totalNumberOfTrades++;
            totalProfitLoss += t.NetProfit();

            if (t.NetProfit() > 0)
            {
                numberOfWinningTrades++;
                totalProfit += t.NetProfit();
                averageProfit += (t.NetProfit() - averageProfit) / numberOfWinningTrades;

                if (t.NetProfit() > largestProfit)
                {
                    largestProfit = t.NetProfit();
                }

                if (totalProfitLoss > maxTotalProfitLoss)
                {
                    maxTotalProfitLoss = totalProfitLoss;

                    lastPeakTime = t.ExitTimeUtc;
                    isInDrawdown = false;
                }
            }
            else
            {
                numberOfLosingTrades++;
                totalLoss += t.NetProfit();
                var prevAverageLoss = averageLoss;
                averageLoss += (t.NetProfit() - averageLoss) / numberOfLosingTrades;

                if (t.NetProfit() < largestLoss)
                {
                    largestLoss = t.NetProfit();
                }

                if (totalProfitLoss - maxTotalProfitLoss < maxClosedTradeDrawdown)
                {
                    maxClosedTradeDrawdown = totalProfitLoss - maxTotalProfitLoss;
                }

                isInDrawdown = true;
            }

            var prevAverageProfitLoss = averageProfitLoss;
            averageProfitLoss += (t.NetProfit() - averageProfitLoss) / totalNumberOfTrades;

            totalCommission += t.Commission;
            totalTax += t.Tax;
            totalSwap += t.Swap;
        }


        var profitLossRatio = averageLoss == 0 ? 0 : averageProfit / Math.Abs(averageLoss);
        var winLossRatio = totalNumberOfTrades == 0 ? 0 : (numberOfLosingTrades > 0 ? (decimal)numberOfWinningTrades / numberOfLosingTrades : 10);
        var winRate = totalNumberOfTrades > 0 ? (decimal)numberOfWinningTrades / totalNumberOfTrades : 0;
        var profitFactor = totalProfit == 0 ? 0 : (totalLoss < 0 ? totalProfit / Math.Abs(totalLoss) : 10);
        // var sharpeRatio = profitLossStandardDeviation > 0 ? averageProfitLoss / profitLossStandardDeviation : 0;
        // var sortinoRatio = profitLossStadardDeviation > 0 ? averageProfitLoss / profitLossDownsideDeviation : 0;
        var profitToMaxClosedTradeDrawdownRatio = totalProfitLoss == 0 ? 0 :
            (maxClosedTradeDrawdown < 0 ? totalProfitLoss / Math.Abs(maxClosedTradeDrawdown) : 10);

        return new AccountPerformanceDto
        {
            TotalNumberOfTrades = totalNumberOfTrades,
            NumberOfWinningTrades = numberOfWinningTrades,
            NumberOfLosingTrades = numberOfLosingTrades,

            TotalProfitLoss = totalProfitLoss,
            TotalProfit = totalProfit,
            TotalLoss = totalLoss,

            AverageProfitLoss = averageProfitLoss,
            LargeProfit = largestProfit,
            LargeLoss = largestLoss,

            AverageProfit = averageProfit,
            AverageLoss = averageLoss,

            TotalCommission = totalCommission,
            TotalTax = totalTax,
            TotalSwap = totalSwap,

            ProfitLossRatio = profitLossRatio,
            WinLossRatio = winLossRatio,
            WinRate = winRate,
            ProfitFactor = profitFactor,

            MaximumClosedTradeDrawdown = maxClosedTradeDrawdown,
            ProfitToMaxClosedTradeDrawdownRatio = profitToMaxClosedTradeDrawdownRatio,
        };
    }
}

internal static class TradeExtensions
{
    internal static decimal NetProfit(this Trade trade) =>
        trade.ProfitLoss - (trade.Commission + trade.Tax + trade.Swap);

    internal static bool IsWinningTrade(this Trade trade) =>
        trade.NetProfit() > 0;

    internal static bool IsLosingTrade(this Trade trade) =>
        trade.NetProfit() <= 0;
}