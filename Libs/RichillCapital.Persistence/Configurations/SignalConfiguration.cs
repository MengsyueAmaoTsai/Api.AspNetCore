using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;

namespace RichillCapital.Persistence.Configurations;

internal sealed class SignalConfiguration : IEntityTypeConfiguration<Signal>
{
    public void Configure(EntityTypeBuilder<Signal> builder)
    {
        builder
            .ToTable("signals")
            .HasKey(signal => signal.Id);

        builder
            .Property(signal => signal.Id)
            .HasColumnName("id")
            .HasMaxLength(36)
            .HasConversion(
                id => id.Value,
                value => SignalId.From(value.ToString()).Value);
    }
}