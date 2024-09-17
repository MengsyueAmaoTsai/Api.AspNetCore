using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
using RichillCapital.Persistence;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class ExecutionConfiguration :
    IEntityTypeConfiguration<Execution>
{
    public void Configure(EntityTypeBuilder<Execution> builder)
    {
        builder
            .HasKey(execution => execution.Id);

        builder
            .Property(execution => execution.Id)
            .HasMaxLength(ExecutionId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => ExecutionId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(execution => execution.AccountId)
            .HasMaxLength(AccountId.MaxLength)
            .HasConversion(
                accountId => accountId.Value,
                value => AccountId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(execution => execution.OrderId)
            .HasMaxLength(OrderId.MaxLength)
            .HasConversion(
                orderId => orderId.Value,
                value => OrderId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(execution => execution.Symbol)
            .HasMaxLength(Symbol.MaxLength)
            .HasConversion(
                symbol => symbol.Value,
                value => Symbol.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(execution => execution.TradeType)
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(execution => execution.OrderType)
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(execution => execution.TimeInForce)
            .HasEnumerationValueConversion()
            .IsRequired();
    }
}