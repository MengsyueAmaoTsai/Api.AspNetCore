using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
using RichillCapital.Domain.Users;

namespace RichillCapital.Persistence.Configurations;

internal sealed class AccountConfiguration :
    IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder
            .ToTable("accounts")
            .HasKey(account => account.Id);

        builder
            .Property(account => account.Id)
            .HasColumnName("id")
            .HasMaxLength(AccountId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => AccountId.From(value).Value)
            .IsRequired();

        builder
            .Property(account => account.UserId)
            .HasColumnName("user_id")
            .HasMaxLength(UserId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => UserId.From(value).Value)
            .IsRequired();

        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey("user_id");
    }
}