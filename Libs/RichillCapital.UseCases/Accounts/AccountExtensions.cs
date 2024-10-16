using RichillCapital.Domain;

namespace RichillCapital.UseCases.Accounts;

internal static class AccountExtensions
{
    internal static AccountDto ToDto(this Account account) =>
        new()
        {
            Id = account.Id.Value,
            UserId = account.UserId.Value,
            ConnectionName = account.ConnectionName,
            Alias = account.Alias,
            Currency = account.Currency.Name,
            CreatedTimeUtc = account.CreatedTimeUtc,
        };
}