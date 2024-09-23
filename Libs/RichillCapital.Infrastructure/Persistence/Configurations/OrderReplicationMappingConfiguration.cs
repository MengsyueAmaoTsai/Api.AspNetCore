using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class OrderReplicationMappingConfiguration :
    IEntityTypeConfiguration<OrderReplicationMapping>
{
    public void Configure(EntityTypeBuilder<OrderReplicationMapping> builder)
    {
        builder
            .HasKey(mapping => mapping.Id);

        builder
            .Property(mapping => mapping.Id)
            .HasMaxLength(OrderReplicationMappingId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => OrderReplicationMappingId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(mapping => mapping.SourceSymbol)
            .HasMaxLength(Symbol.MaxLength)
            .HasConversion(
                symbol => symbol.Value,
                value => Symbol.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(mapping => mapping.DestinationSymbol)
            .HasMaxLength(Symbol.MaxLength)
            .HasConversion(
                symbol => symbol.Value,
                value => Symbol.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(mapping => mapping.DestinationAccountId)
            .HasMaxLength(AccountId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => AccountId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .HasOne<SignalReplicationPolicy>()
            .WithMany(policy => policy.ReplicationMappings)
            .HasForeignKey(mapping => mapping.SignalReplicationPolicyId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne(mapping => mapping.DestinationAccount)
            .WithMany()
            .HasForeignKey(mapping => mapping.DestinationAccountId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(
        [
            CreateMapping(
                id: "1",
                policyId: "1",
                sourceSymbol: "BINANCE:BTCUSDT",
                destinationSymbol: "BINANCE:BTCUSDT",
                destinationAccountId: "000-8283782"),
        ]);
    }

    private static OrderReplicationMapping CreateMapping(
        string id,
        string policyId,
        string sourceSymbol,
        string destinationSymbol,
        string destinationAccountId) =>
        OrderReplicationMapping.Create(
            OrderReplicationMappingId.From(id).ThrowIfFailure().Value,
            SignalReplicationPolicyId.From(policyId).ThrowIfFailure().Value,
            Symbol.From(sourceSymbol).ThrowIfFailure().Value,
            Symbol.From(destinationSymbol).ThrowIfFailure().Value,
            AccountId.From(destinationAccountId).ThrowIfFailure().Value)
        .ThrowIfError()
        .Value;
}