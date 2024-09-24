using RichillCapital.Domain.Files;
using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Errors;

public static class FileErrors
{
    public static Error NotFound(FileEntryId id) =>
        Error.NotFound($"File with id {id} was not found");
}