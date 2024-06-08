using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Files.Update;

internal sealed class UpdateFileCommandHandler(
    IRepository<FileEntry> _fileRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<UpdateFileCommand, ErrorOr<FileEntryDto>>
{
    public async Task<ErrorOr<FileEntryDto>> Handle(
        UpdateFileCommand command,
        CancellationToken cancellationToken)
    {
        var idResult = FileEntryId.From(command.Id);

        if (idResult.IsFailure)
        {
            return idResult.Error
                .ToErrorOr<FileEntryDto>();
        }

        var maybeFileEntry = await _fileRepository.GetByIdAsync(command.Id, cancellationToken);

        if (maybeFileEntry.IsNull)
        {
            return Error
                .NotFound($"File with ID {command.Id} not found")
                .ToErrorOr<FileEntryDto>();
        }

        var fileEntry = maybeFileEntry.Value;

        fileEntry.WithName(command.Name);
        fileEntry.WithDescription(command.Description);

        _fileRepository.Update(fileEntry);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return fileEntry
            .ToDto()
            .ToErrorOr();
    }
}