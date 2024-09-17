using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
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
            .HasOne(account => account.User)
            .WithMany(user => user.Accounts)
            .HasForeignKey(account => account.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
        [
            CreateAccount(
                "1",
                "1",
                "Primary",
                "USD",
                DateTimeOffset.UtcNow),
        ]);
    }

    private static Account CreateAccount(
        string id,
        string userId,
        string alias,
        string currency,
        DateTimeOffset createdTimeUtc) =>
        Account
            .Create(
                AccountId.From(id).ThrowIfFailure().Value,
                UserId.From(userId).ThrowIfFailure().Value,
                alias,
                currency,
                createdTimeUtc)
            .ThrowIfError()
            .Value;
}