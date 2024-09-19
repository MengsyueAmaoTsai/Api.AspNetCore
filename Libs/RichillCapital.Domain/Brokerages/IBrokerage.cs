namespace RichillCapital.Domain.Brokerages;

public interface IBrokerage
{
    Guid Id { get; }
    string Name { get; }
}