using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class SignalSubscriptionConfiguration :
    IEntityTypeConfiguration<SignalSubscription>
{
    public void Configure(EntityTypeBuilder<SignalSubscription> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(subscription => subscription.Id)
            .HasMaxLength(SignalSubscriptionId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => SignalSubscriptionId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(subscription => subscription.UserId)
            .HasMaxLength(UserId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => UserId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(subscription => subscription.SourceId)
            .HasMaxLength(SignalSourceId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => SignalSourceId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder.HasData(
        [
            CreateSubscription(
                id: "1",
                userId: "1",
                sourceId: "TV-Long-Task",
                createdTimeUtc: DateTimeOffset.UtcNow),
        ]);
    }

    private static SignalSubscription CreateSubscription(
        string id,
        string userId,
        string sourceId,
        DateTimeOffset createdTimeUtc) =>
        SignalSubscription
            .Create(
                SignalSubscriptionId.From(id).ThrowIfFailure().Value,
                UserId.From(userId).ThrowIfFailure().Value,
                SignalSourceId.From(sourceId).ThrowIfFailure().Value,
                createdTimeUtc)
            .ThrowIfError()
            .Value;
}