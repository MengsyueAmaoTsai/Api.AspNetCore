using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Files.Upload;

public sealed record UploadFileCommand :
    ICommand<ErrorOr<FileEntryId>>
{
    public required string Name { get; init; }

    public required string Description { get; init; }

    public required long Size { get; init; }

    public required DateTimeOffset UploadedTime { get; init; }

    public required string FileName { get; init; }

    public required bool Encrypted { get; init; }

    public required Stream Stream { get; init; }
}
