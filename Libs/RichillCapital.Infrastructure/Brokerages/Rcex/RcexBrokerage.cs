using RichillCapital.Domain.Brokerages;

namespace RichillCapital.Infrastructure.Brokerages.Rcex;

public sealed class RcexBrokerage : IBrokerage
{
    internal RcexBrokerage(
        Guid id,
        string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; private init; }

    public string Name { get; private init; }
}