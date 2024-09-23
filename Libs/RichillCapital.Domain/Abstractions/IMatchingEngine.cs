namespace RichillCapital.Domain.Abstractions;

public interface IMatchingEngine
{
    void MatchOrder(Order order);
}
