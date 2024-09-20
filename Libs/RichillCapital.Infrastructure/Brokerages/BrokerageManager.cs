using RichillCapital.Domain.Brokerages;

namespace RichillCapital.Infrastructure.Brokerages;

internal sealed class BrokerageManager(
    IBrokerageCollection _brokerages) :
    IBrokerageManager
{
    public IReadOnlyCollection<IBrokerage> ListAll() =>
        _brokerages.All;
}