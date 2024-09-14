using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class ExecutionConfiguration : IEntityTypeConfiguration<Execution>
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
    }
}