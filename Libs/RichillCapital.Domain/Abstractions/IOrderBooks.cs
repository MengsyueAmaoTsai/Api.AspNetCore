namespace RichillCapital.Domain.Abstractions;

public interface IOrderBooks
{
    (IReadOnlyCollection<(decimal Size, decimal Price)> Bids, IReadOnlyCollection<(decimal Size, decimal Price)> Asks) Get(Symbol symbol);
}

internal sealed class InMemoryOrderBooks : IOrderBooks
{
    public (IReadOnlyCollection<(decimal Size, decimal Price)> Bids, IReadOnlyCollection<(decimal Size, decimal Price)> Asks) Get(Symbol symbol)
    {
        throw new NotImplementedException();
    }
}