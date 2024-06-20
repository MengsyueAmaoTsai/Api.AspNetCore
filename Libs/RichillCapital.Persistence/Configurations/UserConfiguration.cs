using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain.Users;

namespace RichillCapital.Persistence.Configurations;

internal sealed class UserConfiguration :
    IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("users")
            .HasKey(user => user.Id);

        builder 
            .Property(user => user.Id)
            .HasColumnName("id")
            .HasMaxLength(UserId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => UserId.From(value).Value)
            .IsRequired();
        
        builder
            .Property(user => user.Name)
            .HasColumnName("name")
            .HasMaxLength(UserName.MaxLength)
            .HasConversion(
                name => name.Value,
                value => UserName.From(value).Value)
            .IsRequired();

        builder
            .Property(user => user.Email)
            .HasColumnName("email")
            .HasMaxLength(Email.MaxLength)
            .HasConversion(
                email => email.Value,
                value => Email.From(value).Value)
            .IsRequired();
    }
}
