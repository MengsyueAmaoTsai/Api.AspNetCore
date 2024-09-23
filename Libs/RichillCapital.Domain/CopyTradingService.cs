using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.SharedKernel.Specifications;
using RichillCapital.SharedKernel.Specifications.Builders;

namespace RichillCapital.Domain;

internal sealed class CopyTradingService(
    ILogger<CopyTradingService> _logger,
    IReadOnlyRepository<SignalReplicationPolicy> _signalReplicationPolicyRepository,
    IBrokerageManager _brokerageManager) :
    ICopyTradingService
{
    public async Task<Result> ReplicateSignalAsync(
        Signal signal,
        CancellationToken cancellationToken = default)
    {
        var policies = await _signalReplicationPolicyRepository.ListAsync(
            new SignalReplicationsSpecification(),
            cancellationToken);

        foreach (var policy in policies)
        {
            var mapping = policy.ReplicationMappings.FirstOrDefault(m => m.SourceSymbol == signal.Symbol);

            if (mapping is null)
            {
                continue;
            }

            var submitResult = await _brokerageManager
                .SubmitOrderAsync(
                    mapping.DestinationAccount.ConnectionName,
                    mapping.DestinationSymbol,
                    signal.TradeType,
                    OrderType.Market,
                    TimeInForce.ImmediateOrCancel,
                    signal.Quantity * policy.Multiplier,
                    OrderId.NewOrderId().Value,
                    cancellationToken);

            if (submitResult.IsFailure)
            {
                _logger.LogWarning(
                    "Failed to submit order for signal {SignalId} to account {AccountId}. Error: {Error}",
                    signal.Id,
                    mapping.DestinationAccount.Id,
                    submitResult.Error);
            }
        }

        return Result.Success;
    }
}

public sealed class SignalReplicationsSpecification : Specification<SignalReplicationPolicy>
{
    public SignalReplicationsSpecification()
    {
        Query.Include(p => p.ReplicationMappings)
            .ThenInclude(m => m.DestinationAccount);
    }
}

