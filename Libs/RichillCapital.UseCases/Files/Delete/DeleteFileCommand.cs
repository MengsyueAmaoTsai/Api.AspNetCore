using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Files.Delete;

public sealed record DeleteFileCommand :
    ICommand<Result>
{
    public required Guid FileId { get; init; }
}
