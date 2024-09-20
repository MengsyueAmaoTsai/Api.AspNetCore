using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.WatchLists.Commands;

public sealed record UpdateWatchListCommand :
    ICommand<ErrorOr<WatchListDto>>
{
    public required string WatchListId { get; init; }
    public required string Name { get; init; }
}
