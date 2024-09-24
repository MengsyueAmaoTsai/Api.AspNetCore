using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain.Files;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class FileEntryConfiguration : IEntityTypeConfiguration<FileEntry>
{
    public void Configure(EntityTypeBuilder<FileEntry> builder)
    {
        builder
            .HasKey(file => file.Id);

        builder
            .Property(file => file.Id)
            .HasMaxLength(FileEntryId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => FileEntryId.From(value).ThrowIfFailure().Value)
            .IsRequired();
    }
}