using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class SignalSourceConfiguration : IEntityTypeConfiguration<SignalSource>
{
    public void Configure(EntityTypeBuilder<SignalSource> builder)
    {
        builder
            .HasKey(source => source.Id);

        builder
            .Property(source => source.Id)
            .HasMaxLength(SignalSourceId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => SignalSourceId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder.HasData(
        [
            CreateSignalSource(
                id: "TV-Long-Task",
                name: "TradingView Long Task",
                description: "TradingView Long Task Signal Source",
                createdTimeUtc: DateTimeOffset.UtcNow),
        ]);
    }

    private static SignalSource CreateSignalSource(
        string id,
        string name,
        string description,
        DateTimeOffset createdTimeUtc) => SignalSource
        .Create(
            SignalSourceId.From(id).ThrowIfFailure().Value,
            name,
            description,
            createdTimeUtc)
        .ThrowIfError().Value;
}