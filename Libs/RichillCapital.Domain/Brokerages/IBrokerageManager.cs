namespace RichillCapital.Domain.Brokerages;

public interface IBrokerageManager
{
    IReadOnlyCollection<IBrokerage> ListAll();
}