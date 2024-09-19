using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.WatchLists.Commands;

public sealed record CreateWatchListCommand : ICommand<ErrorOr<WatchListId>>
{
    public required string UserId { get; init; }
    public required string Name { get; init; }
}
