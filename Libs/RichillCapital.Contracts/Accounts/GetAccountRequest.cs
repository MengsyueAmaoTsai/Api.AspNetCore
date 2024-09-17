using Microsoft.AspNetCore.Mvc;

namespace RichillCapital.Contracts.Accounts;

public sealed record GetAccountRequest
{
    [FromRoute(Name = "accountId")]
    public required string AccountId { get; init; }
}