namespace RichillCapital.Contracts.Accounts;

public sealed record CreateAccountRequest
{
    public required string UserId { get; init; }
    public required string ConnectionName { get; init; }
    public required string Alias { get; init; }
    public required string Currency { get; init; }
}
