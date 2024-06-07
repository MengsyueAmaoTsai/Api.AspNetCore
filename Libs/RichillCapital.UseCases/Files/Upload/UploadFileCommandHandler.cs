
using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Files.Upload;

internal sealed class UploadFileCommandHandler :
    ICommandHandler<UploadFileCommand, ErrorOr<FileEntryId>>
{
    public Task<ErrorOr<FileEntryId>> Handle(UploadFileCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}