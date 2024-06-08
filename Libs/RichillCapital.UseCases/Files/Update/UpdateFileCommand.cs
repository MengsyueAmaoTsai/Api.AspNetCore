using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Files.Update;

public sealed record UpdateFileCommand :
    ICommand<ErrorOr<FileEntryDto>>
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }
}
