using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class OrderReplicationMapping : Entity<OrderReplicationMappingId>
{
    private OrderReplicationMapping(
        OrderReplicationMappingId id,
        SignalReplicationPolicyId signalReplicationPolicyId,
        Symbol sourceSymbol,
        Symbol destinationSymbol,
        AccountId destinationAccountId)
        : base(id)
    {
        SignalReplicationPolicyId = signalReplicationPolicyId;
        SourceSymbol = sourceSymbol;
        DestinationSymbol = destinationSymbol;
        DestinationAccountId = destinationAccountId;
    }

    public SignalReplicationPolicyId SignalReplicationPolicyId { get; private set; }
    public Symbol SourceSymbol { get; private set; }
    public Symbol DestinationSymbol { get; private set; }
    public AccountId DestinationAccountId { get; private set; }

    public Account DestinationAccount { get; private set; }

    public static ErrorOr<OrderReplicationMapping> Create(
        OrderReplicationMappingId id,
        SignalReplicationPolicyId signalReplicationPolicyId,
        Symbol sourceSymbol,
        Symbol destinationSymbol,
        AccountId destinationAccountId)
    {
        var mapping = new OrderReplicationMapping(
            id,
            signalReplicationPolicyId,
            sourceSymbol,
            destinationSymbol,
            destinationAccountId);

        return ErrorOr<OrderReplicationMapping>.With(mapping);
    }
}
