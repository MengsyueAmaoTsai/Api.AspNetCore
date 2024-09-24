using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.SignalSources.Queries;

internal sealed class GetSignalSourcePerformanceQueryHandler(
    ILogger<GetSignalSourcePerformanceQueryHandler> _logger,
    IReadOnlyRepository<SignalSource> _signalSourceRepository,
    IReadOnlyRepository<Trade> _tradeRepository) :
    IQueryHandler<GetSignalSourcePerformanceQuery, ErrorOr<SignalSourcePerformanceDto>>
{
    public async Task<ErrorOr<SignalSourcePerformanceDto>> Handle(
        GetSignalSourcePerformanceQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = SignalSourceId.From(query.SignalSourceId);

        if (validationResult.IsFailure)
        {
            return ErrorOr<SignalSourcePerformanceDto>.WithError(validationResult.Error);
        }

        var id = validationResult.Value;

        var maybeSource = await _signalSourceRepository.GetByIdAsync(id, cancellationToken);

        if (maybeSource.IsNull)
        {
            return ErrorOr<SignalSourcePerformanceDto>.WithError(SignalSourceErrors.NotFound(id));
        }

        var source = maybeSource.Value;

        var accounts = source.ReplicationPolicies
            .SelectMany(p => p.ReplicationMappings)
            .Select(mapping => mapping.DestinationAccountId)
            .ToList();

        var trades = await _tradeRepository
            .ListAsync(t => accounts.Contains(t.AccountId), cancellationToken);

        foreach (var trade in trades)
        {
            _logger.LogInformation(
                "Trade - Id: {TradeId}, AccountId: {AccountId}, Symbol: {Symbol}",
                trade.Id,
                trade.AccountId,
                trade.Symbol);
        }

        _logger.LogInformation("Total trades: {TotalTrades}", trades.Count);

        return ErrorOr<SignalSourcePerformanceDto>.With(new SignalSourcePerformanceDto
        {
        });
    }
}