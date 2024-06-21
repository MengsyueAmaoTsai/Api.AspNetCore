using RichillCapital.Domain;

namespace RichillCapital.UseCases.Accounts;

public sealed record AccountDto
{
    public required string Id { get; init; }

    public required string UserId { get; init; }

    public required string Name { get; init; }
}

internal static class AccountExtensions
{
    public static AccountDto ToDto(this Account account) =>
        new()
        {
            Id = account.Id.Value,
            UserId = account.UserId.Value,
            Name = account.Name,
        };
}