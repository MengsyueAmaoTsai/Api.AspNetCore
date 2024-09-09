using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class SignalConfiguration : IEntityTypeConfiguration<Signal>
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
                value => SignalId.From(value).ThrowIfFailure().Value)
            .IsRequired();
    }
}