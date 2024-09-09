using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
using RichillCapital.Persistence;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .HasKey(order => order.Id);

        builder
            .Property(order => order.Id)
            .HasMaxLength(OrderId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => OrderId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(order => order.TradeType)
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(order => order.Symbol)
            .HasMaxLength(Symbol.MaxLength)
            .HasConversion(
                symbol => symbol.Value,
                value => Symbol.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(order => order.Type)
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(order => order.TimeInForce)
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(order => order.Status)
            .HasEnumerationValueConversion()
            .IsRequired();
    }
}