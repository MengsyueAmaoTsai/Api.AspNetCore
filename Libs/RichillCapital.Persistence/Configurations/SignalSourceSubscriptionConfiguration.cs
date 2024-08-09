using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
using RichillCapital.Persistence.Seeds;

namespace RichillCapital.Persistence.Configurations;

internal sealed class SignalSourceSubscriptionConfiguration :
    IEntityTypeConfiguration<SignalSourceSubscription>
{
    public void Configure(EntityTypeBuilder<SignalSourceSubscription> builder)
    {
        builder
            .HasKey(subscription => subscription.Id);

        builder
            .Property(subscription => subscription.Id)
            .HasMaxLength(SignalSourceSubscriptionId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => SignalSourceSubscriptionId.From(value).Value)
            .IsRequired();

        // Relationships
        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(subscription => subscription.UserId);

        builder
            .HasOne(subscription => subscription.Source)
            .WithMany(source => source.Subscriptions)
            .HasForeignKey(subscription => subscription.SourceId);

        // Seed
        builder
            .HasData(Seed.CreateSignalSourceSubscriptions());
    }
}