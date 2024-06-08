using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;

namespace RichillCapital.Persistence.Configurations;

internal sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder
            .ToTable("audit-logs")
            .HasKey(log => log.Id);

        builder
            .Property(log => log.Id)
            .HasColumnName("id")
            .HasMaxLength(AuditLogId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => AuditLogId.From(value).Value)
            .IsRequired();
    }
}