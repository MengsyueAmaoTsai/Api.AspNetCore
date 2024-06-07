using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;

namespace RichillCapital.Persistence.Configurations;

internal sealed class FileEntryConfiguration : IEntityTypeConfiguration<FileEntry>
{
    public void Configure(EntityTypeBuilder<FileEntry> builder)
    {
        builder
            .ToTable("files")
            .HasKey(file => file.Id);

        builder
            .Property(file => file.Id)
            .HasColumnName("id")
            .HasMaxLength(FileEntryId.MaxLength)
            .HasConversion(id => id.Value, value => FileEntryId.From(value).Value)
            .IsRequired();
    }
}