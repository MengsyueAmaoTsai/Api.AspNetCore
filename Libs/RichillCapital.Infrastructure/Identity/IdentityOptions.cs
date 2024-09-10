namespace RichillCapital.Infrastructure.Identity;

public sealed record IdentityOptions
{
    internal const string SectionKey = "Identity";

    public required string Authority { get; init; }
    public required string Audience { get; init; }
    public required bool RequireHttpsMetadata { get; init; }
}
