using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.WatchLists.Commands;

public sealed record DeleteWatchListCommand : ICommand<Result>
{
    public required string WatchListId { get; init; }
}
