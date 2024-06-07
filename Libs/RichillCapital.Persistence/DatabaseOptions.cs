namespace RichillCapital.Persistence;

public sealed record DatabaseOptions
{
    internal const string SectionKey = "Database";

    public required string ConnectionString { get; init; }
}