using FluentValidation;

using RichillCapital.Infrastructure.Storage.Local;

namespace RichillCapital.Infrastructure.Storage;

public sealed record StorageOptions
{
    internal const string SectionKey = "Storage";

    public required LocalStorageOptions Local { get; init; }
}

internal sealed class StorageOptionsValidator : AbstractValidator<StorageOptions>
{
    public StorageOptionsValidator()
    {
    }
}

