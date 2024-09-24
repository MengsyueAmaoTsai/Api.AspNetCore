using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
using RichillCapital.Persistence;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder
            .HasKey(account => account.Id);

        builder
            .Property(account => account.Id)
            .HasMaxLength(AccountId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => AccountId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(account => account.UserId)
            .HasMaxLength(UserId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => UserId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(account => account.Currency)
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .HasOne<User>()
            .WithMany(user => user.Accounts)
            .HasForeignKey(account => account.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasData([.. CreateAccounts()]);
    }

    private static IEnumerable<Account> CreateAccounts()
    {
        var userId = "1";

        yield return CreateAccount(
                id: "SIM-0000000000000000",
                userId: userId,
                connectionName: "RichillCapital.Exchange",
                alias: "測試模擬帳戶",
                currency: Currency.TWD,
                createdTimeUtc: DateTimeOffset.UtcNow);

        yield return CreateAccount(
                id: "000-8283782",
                userId: userId,
                connectionName: "RichillCapital.Kgi",
                alias: "凱基期貨",
                currency: Currency.TWD,
                createdTimeUtc: DateTimeOffset.UtcNow);
    }

    private static Account CreateAccount(
        string id,
        string userId,
        string connectionName,
        string alias,
        Currency currency,
        DateTimeOffset createdTimeUtc) =>
        Account.Create(
            AccountId.From(id).ThrowIfFailure().Value,
            UserId.From(userId).ThrowIfFailure().Value,
            connectionName,
            alias,
            currency,
            createdTimeUtc).ThrowIfError().Value;
}