using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class WatchListConfiguration :
    IEntityTypeConfiguration<WatchList>
{
    public void Configure(EntityTypeBuilder<WatchList> builder)
    {
        builder
            .HasKey(list => list.Id);

        builder
            .Property(list => list.Id)
            .HasMaxLength(WatchListId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => WatchListId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(list => list.UserId)
            .HasMaxLength(UserId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => UserId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .HasOne<User>()
            .WithMany(user => user.WatchLists)
            .HasForeignKey(list => list.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasData(
        [
            CreateWatchList(
                id: "1",
                userId: "1",
                name: "Watch List 1"),
        ]);
    }

    private static WatchList CreateWatchList(
        string id,
        string userId,
        string name) =>
        WatchList
            .Create(
                WatchListId.From(id).ThrowIfFailure().Value,
                UserId.From(userId).ThrowIfFailure().Value,
                name)
            .ThrowIfError()
            .Value;
}