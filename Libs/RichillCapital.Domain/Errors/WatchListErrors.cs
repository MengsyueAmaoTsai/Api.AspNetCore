using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Errors;

public static class WatchListErrors
{
    public static Error NotFound(WatchListId id) =>
        Error.NotFound("WatchLists.NotFound", $"Watch list with id {id} was not found.");
}