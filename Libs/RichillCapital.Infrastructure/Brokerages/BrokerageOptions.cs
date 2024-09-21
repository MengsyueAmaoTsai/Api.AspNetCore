namespace RichillCapital.Infrastructure.Brokerages;

public sealed record BrokerageOptions
{
    internal const string SectionKey = "Brokerages";

    public required IEnumerable<BrokerageProfile> Profiles { get; init; }
}
