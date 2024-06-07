using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.Domain.Storage;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Files.Upload;

internal sealed class UploadFileCommandHandler(
    IFileStorageManager _fileManager,
    IRepository<FileEntry> _fileRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<UploadFileCommand, ErrorOr<FileEntryId>>
{
    public async Task<ErrorOr<FileEntryId>> Handle(
        UploadFileCommand command,
        CancellationToken cancellationToken)
    {
        var id = FileEntryId.NewFileEntryId();
        var location = DateTimeOffset.UtcNow.ToString("yyyy-MM-dd/") + id.Value;

        var errorOrFileEntry = FileEntry.Create(
            id,
            command.Name,
            command.Description,
            command.Size,
            command.UploadedTime,
            location,
            command.FileName,
            command.Encrypt,
            string.Empty,
            string.Empty);

        if (errorOrFileEntry.HasError)
        {
            return errorOrFileEntry.Errors
                .ToErrorOr<FileEntryId>();
        }

        var fileEntry = errorOrFileEntry.Value;

        using var stream = command.Stream;

        if (command.Encrypt)
        {
            // var key = SymmetricCrypto.GenerateKey(32);
            // var iv = SymmetricCrypto.GenerateKey(16);

            // using var encryptedStream = new MemoryStream(stream
            //         .UseAES(key)
            //         .WithCipher(CipherMode.CBC)
            //         .WithIV(iv)
            //         .WithPadding(PaddingMode.PKCS7)
            //         .Encrypt());

            // await _fileManager.CreateAsync(fileEntry, encryptedStream, cancellationToken);

            // var masterEncryptionKey = "const";

            // var encryptedKey = key
            //     .UseAES(masterEncryptionKey.FromBase64String())
            //     .WithCipher(CipherMode.CBC)
            //     .WithIV(iv)
            //     .WithPadding(PaddingMode.PKCS7)
            //     .Encrypt();

            // fileEntry.EncryptionKey = encryptedKey.ToBase64String();
            // fileEntry.EncryptionIV = iv.ToBase64String();
        }
        else
        {
            await _fileManager.CreateAsync(fileEntry, stream, cancellationToken);
        }

        _fileRepository.Add(fileEntry);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return fileEntry.Id
            .ToErrorOr();
    }
}