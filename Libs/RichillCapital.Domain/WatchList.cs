using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class WatchList : Entity<WatchListId>
{
    private WatchList(
        WatchListId id,
        UserId userId,
        string name)
        : base(id)
    {
        UserId = userId;
        Name = name;
    }

    public UserId UserId { get; private set; }
    public string Name { get; private set; }

    public static ErrorOr<WatchList> Create(
        WatchListId id,
        UserId userId,
        string name)
    {
        var watchList = new WatchList(
            id,
            userId,
            name);

        return ErrorOr<WatchList>.With(watchList);
    }

    public Result Update(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure(Error.Invalid(
                $"{nameof(name)} cannot be empty."));
        }

        Name = name;

        return Result.Success;
    }
}
