using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.Domain.Common.Storage;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Files.Download;

internal sealed class DownloadFileQueryHandler(
    IReadOnlyRepository<FileEntry> _fileRepository,
    IFileStorageManager _fileManager) :
    IQueryHandler<DownloadFileQuery, ErrorOr<FileDto>>
{
    public async Task<ErrorOr<FileDto>> Handle(
        DownloadFileQuery query,
        CancellationToken cancellationToken)
    {
        var fileIdResult = FileEntryId.From(query.FileId);

        if (fileIdResult.IsFailure)
        {
            return fileIdResult.Error
                .ToErrorOr<FileDto>();
        }

        var maybeFileEntry = await _fileRepository.GetByIdAsync(fileIdResult.Value, cancellationToken);

        if (maybeFileEntry.IsNull)
        {
            return Error
                .NotFound($"File with id {query.FileId} not found")
                .ToErrorOr<FileDto>();
        }

        var fileEntry = maybeFileEntry.Value;
        var rawData = await _fileManager.ReadAsync(fileEntry, cancellationToken);
        var content = rawData;

        if (fileEntry.Encrypt)
        {
            // var masterEncryptionKey = _options.Storage.MasterEncryptionKey;
            // var encryptionKey = fileEntry.EncryptionKey.FromBase64String()
            //           .UseAES(masterEncryptionKey.FromBase64String())
            //           .WithCipher(CipherMode.CBC)
            //           .WithIV(fileEntry.EncryptionIV.FromBase64String())
            //           .WithPadding(PaddingMode.PKCS7)
            //           .Decrypt();

            // content = fileEntry.FileLocation != "Fake.txt"
            //     ? rawData
            //     .UseAES(encryptionKey)
            //     .WithCipher(CipherMode.CBC)
            //     .WithIV(fileEntry.EncryptionIV.FromBase64String())
            //     .WithPadding(PaddingMode.PKCS7)
            //     .Decrypt()
            //     : rawData;
            // return decryptedContent;
        }

        return new FileDto
        {
            Name = fileEntry.FileName,
            Content = content,
        }.ToErrorOr();
    }
}