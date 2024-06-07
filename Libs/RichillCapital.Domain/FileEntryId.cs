using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class FileEntryId : SingleValueObject<Guid>
{
    public const int MaxLength = 36;

    private FileEntryId(Guid value) :
        base(value)
    {
    }

    public static FileEntryId NewFileEntryId() => From(Guid.NewGuid()).Value;

    public static Result<FileEntryId> From(Guid value) => value
        .ToResult()
        .Then(id => new FileEntryId(id));
}