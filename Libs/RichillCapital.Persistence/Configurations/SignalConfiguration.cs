using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
using RichillCapital.Persistence.Seeds;

namespace RichillCapital.Persistence.Configurations;

internal sealed class SignalConfiguration :
    IEntityTypeConfiguration<Signal>
{
    public void Configure(EntityTypeBuilder<Signal> builder)
    {
        builder
            .HasKey(signal => signal.Id);

        builder
            .Property(signal => signal.Id)
            .HasMaxLength(SignalId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => SignalId.From(value).ValueOrDefault)
            .IsRequired();


        // Relationships
        builder
            .HasOne(signal => signal.Source)
            .WithMany(source => source.Signals)
            .HasForeignKey(signal => signal.SourceId);

        // Seeds
        builder
            .HasData(Seed.CreateSignals());
    }
}