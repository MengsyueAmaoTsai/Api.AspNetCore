using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
using RichillCapital.Persistence;
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

        builder
            .Property(signal => signal.SourceId)
            .HasMaxLength(SignalSourceId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => SignalSourceId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(signal => signal.Symbol)
            .HasMaxLength(Symbol.MaxLength)
            .HasConversion(
                symbol => symbol.Value,
                value => Symbol.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(signal => signal.TradeType)
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(signal => signal.OrderType)
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(signal => signal.Origin)
            .HasEnumerationValueConversion()
            .IsRequired();
    }
}