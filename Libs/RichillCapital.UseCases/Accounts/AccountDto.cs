namespace RichillCapital.UseCases.Accounts;

public sealed record AccountDto
{
    public required string Id { get; init; }
    public required string UserId { get; init; }
    public required string ConnectionName { get; init; }
    public required string Alias { get; init; }
    public required string Currency { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}
