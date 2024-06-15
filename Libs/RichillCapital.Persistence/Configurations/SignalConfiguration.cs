using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;

namespace RichillCapital.Persistence.Configurations;

internal sealed class SignalConfiguration :
    IEntityTypeConfiguration<Signal>
{
    public void Configure(EntityTypeBuilder<Signal> builder)
    {
        builder
            .ToTable("signals")
            .HasKey(signal => signal.Id);

        builder
            .Property(signal => signal.Id)
            .HasColumnName("id")
            .HasMaxLength(SignalId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => SignalId.From(value).Value)
            .IsRequired();

        builder
            .Property(signal => signal.SourceId)
            .HasColumnName("source_id")
            .HasMaxLength(SignalSourceId.MaxLength)
            .HasConversion(
                sourceId => sourceId.Value,
                value => SignalSourceId.From(value).Value)
            .IsRequired();

        builder
            .HasOne<SignalSource>()
            .WithMany()
            .HasForeignKey("source_id");
    }
}