using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;

namespace RichillCapital.Persistence.Configurations;

internal sealed class SignalSourceConfiguration :
    IEntityTypeConfiguration<SignalSource>
{
    public void Configure(EntityTypeBuilder<SignalSource> builder)
    {
        builder
            .ToTable("signal_sources")
            .HasKey(source => source.Id);

        builder
            .Property(source => source.Id)
            .HasColumnName("id")
            .HasMaxLength(36)
            .HasConversion(
                id => id.Value,
                value => SignalSourceId.From(value.ToString()).Value);
    }
}