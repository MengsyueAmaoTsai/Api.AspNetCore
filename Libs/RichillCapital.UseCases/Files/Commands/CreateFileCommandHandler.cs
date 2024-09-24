
using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Files.Commands;

internal sealed class CreateFileCommandHandler(
    IFileStorageManager _fileStorageManager) :
    ICommandHandler<CreateFileCommand, Result>
{
    public Task<Result> Handle(
        CreateFileCommand command,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}