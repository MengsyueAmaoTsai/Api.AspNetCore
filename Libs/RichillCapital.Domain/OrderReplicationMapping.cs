using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class OrderReplicationMapping : Entity<OrderReplicationMappingId>
{
    private OrderReplicationMapping(
        OrderReplicationMappingId id,
        SignalReplicationPolicyId signalReplicationPolicyId,
        Symbol sourceSymbol,
        Symbol targetSymbol,
        AccountId targetAccountId)
        : base(id)
    {
        SignalReplicationPolicyId = signalReplicationPolicyId;
        SourceSymbol = sourceSymbol;
        TargetSymbol = targetSymbol;
        TargetAccountId = targetAccountId;
    }

    public SignalReplicationPolicyId SignalReplicationPolicyId { get; private set; }
    public Symbol SourceSymbol { get; private set; }
    public Symbol TargetSymbol { get; private set; }
    public AccountId TargetAccountId { get; private set; }

    public Account TargetAccount { get; private set; }

    public static ErrorOr<OrderReplicationMapping> Create(
        OrderReplicationMappingId id,
        SignalReplicationPolicyId signalReplicationPolicyId,
        Symbol sourceSymbol,
        Symbol targetSymbol,
        AccountId targetAccountId)
    {
        var mapping = new OrderReplicationMapping(
            id,
            signalReplicationPolicyId,
            sourceSymbol,
            targetSymbol,
            targetAccountId);

        return ErrorOr<OrderReplicationMapping>.With(mapping);
    }
}
