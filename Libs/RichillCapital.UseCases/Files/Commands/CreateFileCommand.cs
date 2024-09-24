using RichillCapital.Domain.Files;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Files.Commands;

public sealed record CreateFileCommand : ICommand<ErrorOr<FileEntryId>>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required long Size { get; init; }
    public required string FileName { get; init; }
    public required Stream FileStream { get; init; }
    public required bool Encrypted { get; init; }
}
