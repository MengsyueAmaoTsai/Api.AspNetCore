using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class SignalReplicationPolicyConfiguration :
    IEntityTypeConfiguration<SignalReplicationPolicy>
{
    public void Configure(EntityTypeBuilder<SignalReplicationPolicy> builder)
    {
        builder
            .HasKey(policy => policy.Id);

        builder
            .Property(policy => policy.Id)
            .HasMaxLength(SignalReplicationPolicyId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => SignalReplicationPolicyId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(policy => policy.UserId)
            .HasMaxLength(UserId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => UserId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(policy => policy.SourceId)
            .HasMaxLength(SignalSourceId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => SignalSourceId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder.HasData(
        [
            CreatePolicy(
                id: "1",
                userId: "1",
                sourceId: "TV-Long-Task"),
        ]);
    }

    private static SignalReplicationPolicy CreatePolicy(
        string id,
        string userId,
        string sourceId) =>
        SignalReplicationPolicy
            .Create(
                SignalReplicationPolicyId.From(id).ThrowIfFailure().Value,
                UserId.From(userId).ThrowIfFailure().Value,
                SignalSourceId.From(sourceId).ThrowIfFailure().Value,
                DateTimeOffset.UtcNow)
            .Value;
}