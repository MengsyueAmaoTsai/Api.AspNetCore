using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
using RichillCapital.Persistence;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder
            .HasKey(position => position.Id);

        builder
            .Property(position => position.Id)
            .HasMaxLength(PositionId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => PositionId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(position => position.AccountId)
            .HasMaxLength(AccountId.MaxLength)
            .HasConversion(
                accountId => accountId.Value,
                value => AccountId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(position => position.Symbol)
            .HasMaxLength(Symbol.MaxLength)
            .HasConversion(
                symbol => symbol.Value,
                value => Symbol.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(position => position.Side)
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(position => position.Status)
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .HasOne<Account>()
            .WithMany(account => account.Positions)
            .HasForeignKey(position => position.AccountId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasData(
        [
            .. CreatePositions_KgiFutures()
        ]);
    }

    private static IEnumerable<Position> CreatePositions_KgiFutures()
    {
        var accountId = "000-8283782";
        var symbol = "TAIFEX:TMF";

        yield return CreatePosition(
            id: "PID1",
            accountId: accountId,
            symbol: symbol,
            side: Side.Short,
            quantity: 0,
            averagePrice: 0,
            commission: 64,
            tax: 16,
            swap: decimal.Zero,
            status: PositionStatus.Closed,
            createdTimeUtc: new DateTimeOffset(2024, 9, 12, 11, 18, 32, TimeSpan.Zero));

        yield return CreatePosition(
            id: "PID2",
            accountId: accountId,
            symbol: symbol,
            side: Side.Long,
            quantity: 0,
            averagePrice: 0,
            commission: 32,
            tax: 8,
            swap: decimal.Zero,
            status: PositionStatus.Closed,
            createdTimeUtc: new DateTimeOffset(2024, 9, 13, 1, 56, 55, TimeSpan.Zero));

        yield return CreatePosition(
            id: "PID3",
            accountId: accountId,
            symbol: symbol,
            side: Side.Short,
            quantity: 0,
            averagePrice: 0,
            commission: 32,
            tax: 8,
            swap: decimal.Zero,
            status: PositionStatus.Closed,
            createdTimeUtc: new DateTimeOffset(2024, 9, 13, 1, 58, 26, TimeSpan.Zero));
    }

    private static Position CreatePosition(
        string id,
        string accountId,
        string symbol,
        Side side,
        decimal quantity,
        decimal averagePrice,
        decimal commission,
        decimal tax,
        decimal swap,
        PositionStatus status,
        DateTimeOffset createdTimeUtc) =>
        Position
            .Create(
                PositionId.From(id).ThrowIfFailure().Value,
                AccountId.From(accountId).ThrowIfFailure().Value,
                Symbol.From(symbol).ThrowIfFailure().Value,
                side,
                quantity,
                averagePrice,
                commission,
                tax,
                swap,
                status,
                createdTimeUtc)
            .ThrowIfError()
            .Value;
}