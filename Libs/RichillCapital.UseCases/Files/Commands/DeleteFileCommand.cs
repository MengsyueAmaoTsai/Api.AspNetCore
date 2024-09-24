using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Files.Commands;

public sealed record DeleteFileCommand : ICommand<Result>
{
    public required string FileId { get; init; }
}
