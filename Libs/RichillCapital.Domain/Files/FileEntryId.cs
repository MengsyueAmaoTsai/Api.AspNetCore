using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Files;

public sealed class FileEntryId : SingleValueObject<string>
{
    internal const int MaxLength = 36;

    private FileEntryId(string value)
        : base(value)
    {
    }

    public static Result<FileEntryId> From(string value) =>
        Result<string>
            .With(value)
            .Then(id => new FileEntryId(id));

    public static FileEntryId NewFileEntryId() =>
        From(Guid.NewGuid().ToString()).ThrowIfFailure().Value;
}